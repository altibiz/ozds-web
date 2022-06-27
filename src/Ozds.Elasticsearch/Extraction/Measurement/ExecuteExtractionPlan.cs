using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public AsyncMeasurementExtraction
  ExecuteExtractionPlanAsync(ExtractionPlan plan) =>
    Providers
      .Find(provider => provider.Source == plan.Device.Source)
      .WhenNonNullFinally(provider =>
        plan.Items
          .Select(item => provider
            .GetMeasurementsAsync(
              plan.Device.ToProvisionDevice(),
              item.Period)
            .ForEachAsync(bucket => Logger
              .LogDebug(
                $"Extracted {bucket.Count()} measurements " +
                $"at {bucket.Period} " +
                $"for {plan.Device.Id} " +
                $"from {provider.Source}"))
            .Select(bucket =>
              CreateExtractionItem(
                plan,
                item,
                bucket)))
          .FlattenAsync(),
        AsyncEnumerable.Empty<MeasurementExtractionItem>)
      .To(items =>
        new AsyncMeasurementExtraction
        {
          Device = plan.Device,
          Period = plan.Period,
          Items = items
        });

  public MeasurementExtraction
  ExecuteExtractionPlan(ExtractionPlan plan) =>
    Providers
      .Find(provider => provider.Source == plan.Device.Source)
      .WhenNonNull(provider =>
        plan.Items
          .Select(item => provider
            .GetMeasurements(
              plan.Device.ToProvisionDevice(),
              item.Period)
            .ForEach(bucket => Logger
              .LogDebug(
                $"Extracted {bucket.Count()} measurements " +
                $"at {bucket.Period} " +
                $"for {plan.Device.Id} " +
                $"from {provider.Source}"))
            .Select(bucket =>
              CreateExtractionItem(
                plan,
                item,
                bucket)))
          .Flatten(),
        Enumerable.Empty<MeasurementExtractionItem>)
      .To(items =>
        new MeasurementExtraction
        {
          Device = plan.Device,
          Period = plan.Period,
          Items = items
        });

  private static MeasurementExtractionItem CreateExtractionItem(
      ExtractionPlan plan,
      ExtractionPlanItem item,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    new MeasurementExtractionItem
    {
      Bucket = bucket,
      Original = item,
      Next = PlanNextExtractionItem(plan.Device, item, bucket)
    };

  private static ExtractionPlanItem? PlanNextExtractionItem(
      ExtractionDevice device,
      ExtractionPlanItem last,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    ExtractionPlanItemCompleted(device, last, bucket) switch
    {
      (ExtractionPlanItemError error) =>
        new ExtractionPlanItem
        {
          Due = last.Due + (last.Timeout * Math.Pow(2, last.Retries)),
          Retries = last.Retries + 1,
          Period = bucket.Period,
          Timeout = last.Timeout,
          ShouldValidate = false,
          Error = error
        },
      _ => null
    };

  private static ExtractionPlanItemError? ExtractionPlanItemCompleted(
      ExtractionDevice device,
      ExtractionPlanItem item,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    item.Retries >= device.ExtractionRetries ? null
    : bucket switch
    {
      (Period period,
      null,
      IEnumerable<ExtractionMeasurement> measurements) =>
        (item.ShouldValidate ?
          ExtractedMeasurementsValid(device, period, measurements)
          : null) ??
          ExtractedMeasurementsConsistent(device, period, measurements),
      (_,
      string error,
      _) =>
        new ExtractionPlanItemError(
          ExtractionPlanItemErrorCode.Provider,
          error),
    };

  private static ExtractionPlanItemError? ExtractedMeasurementsConsistent(
      ExtractionDevice device,
      Period period,
      IEnumerable<ExtractionMeasurement> measurements)
  {
    var expected =
      Math.Floor(
        period.Span.TotalSeconds /
        device.MeasurementInterval.TotalSeconds);
    var got = measurements.Count();

    return
      got < expected ?
        new ExtractionPlanItemError(
          ExtractionPlanItemErrorCode.Consistency,
          string.Format(
            "Measurements inconsistent because expected %s but got %s",
            expected,
            got))
      : null;
  }

  // TODO: better description
  private static ExtractionPlanItemError? ExtractedMeasurementsValid(
      ExtractionDevice device,
      Period period,
      IEnumerable<ExtractionMeasurement> measurements) =>
    measurements.All(ExtractionMeasurementExtensions.Validate) ? null
    : new ExtractionPlanItemError(
        ExtractionPlanItemErrorCode.Validation,
        "Measurements invalid");
}
