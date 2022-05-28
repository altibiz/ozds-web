using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public MeasurementExtractionAsync
  ExecuteExtractionPlanAsync(ExtractionPlan plan) =>
    Providers
      .Find(provider => provider.Source == plan.Device.Source)
      .WhenNonNullableFinally(provider =>
        plan.Items
          .Select(item => provider
            .GetMeasurementsAwait(
              plan.Device.ToProvisionDevice(),
              item.Period)
            .ForEach(bucket => Logger
              .LogDebug(
                $"Extracted {bucket.Count()} measurements " +
                $"at {plan.Period} " +
                $"for {plan.Device.Id} " +
                $"from {provider.Source}"))
            .Select(bucket =>
              CreateExtractionItem(
                plan,
                item,
                bucket)))
          .Flatten(),
        Enumerables.EmptyAsync<MeasurementExtractionItem>)
      .WhenNullable(items =>
        new MeasurementExtractionAsync
        {
          Device = plan.Device,
          Period = plan.Period,
          Items = items
        });

  public MeasurementExtraction
  ExecuteExtractionPlan(ExtractionPlan plan) =>
    Providers
      .Find(provider => provider.Source == plan.Device.Source)
      .WhenNonNullable(provider =>
        plan.Items
          .Select(item => provider
            .GetMeasurements(
              plan.Device.ToProvisionDevice(),
              item.Period)
            .ForEach(bucket => Logger
              .LogDebug(
                $"Extracted {bucket.Count()} measurements " +
                $"at {plan.Period} " +
                $"for {plan.Device.Id} " +
                $"from {provider.Source}"))
            .Select(bucket =>
              CreateExtractionItem(
                plan,
                item,
                bucket)))
          .Flatten(),
        Enumerables.Empty<MeasurementExtractionItem>)
      .WhenNullable(items =>
        new MeasurementExtraction
        {
          Device = plan.Device,
          Period = plan.Period,
          Items = items
        });

  private MeasurementExtractionItem CreateExtractionItem(
      ExtractionPlan plan,
      ExtractionPlanItem item,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    new MeasurementExtractionItem
    {
      Bucket = bucket,
      Original = item,
      Next = PlanNextExtractionItem(plan.Device, item, bucket)
    };

  private ExtractionPlanItem? PlanNextExtractionItem(
      ExtractionDevice device,
      ExtractionPlanItem last,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    ExtractionPlanItemCompleted(device, last, bucket) switch
    {
      (string error) =>
        new ExtractionPlanItem
        {
          Due = last.Due + (last.Timeout * Math.Pow(2, last.Retries)),
          Retries = last.Retries + 1,
          Period = bucket.Period,
          Timeout = last.Timeout,
          ShouldValidate = last.ShouldValidate,
          Error = error
        },
      _ => null
    };

  private static string? ExtractionPlanItemCompleted(
      ExtractionDevice device,
      ExtractionPlanItem item,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    item.Retries >= device.ExtractionRetries ? null
    : bucket switch
    {
      (Period period,
      null,
      IEnumerable<ExtractionMeasurement> measurements) =>
        ExtractedMeasurementsConsistent(device, period, measurements) ??
        (item.ShouldValidate ?
          ExtractedMeasurementsValid(device, period, measurements)
          : null),
      _ => bucket.Error
    };

  private static string? ExtractedMeasurementsConsistent(
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
        $"Measurements inconsistent because expected {expected} but got {got}"
      : null;
  }

  private static string? ExtractedMeasurementsValid(
      ExtractionDevice device,
      Period period,
      IEnumerable<ExtractionMeasurement> measurements) =>
    measurements.All(ExtractionMeasurementExtensions.Validate) ? null
    : "Measurements invalid";
}
