using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public Task<ExtractionOutcome>
  ExecuteExtractionPlanAsync(ExtractionPlan plan);

  public ExtractionOutcome
  ExecuteExtractionPlan(ExtractionPlan plan);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public Task<ExtractionOutcome>
  ExecuteExtractionPlanAsync(ExtractionPlan plan) =>
    Providers
      .Find(provider => provider.Source == plan.Device.Source)
      .WhenNonNullableFinallyTask(provider =>
        plan.Items
          .Select(item => provider
            .GetMeasurementsAsync(plan.Device.ToProvisionDevice(), item.Period)
            .Then(buckets => buckets
              .Select(bucket => CreateOutcomeItem(plan, item, bucket))))
          .Await()
          .Then(Enumerables.Flatten),
        Enumerables.Empty<ExtractionOutcomeItem>)
      .Then(items =>
        new ExtractionOutcome
        {
          Device = plan.Device,
          Items = items
        });

  public ExtractionOutcome
  ExecuteExtractionPlan(ExtractionPlan plan) =>
    Providers
      .Find(provider => provider.Source == plan.Device.Source)
      .WhenNonNullable(provider =>
        plan.Items
          .Select(item => provider
            .GetMeasurements(plan.Device.ToProvisionDevice(), item.Period)
            .Select(bucket => CreateOutcomeItem(plan, item, bucket)))
          .Flatten(),
        Enumerables.Empty<ExtractionOutcomeItem>)
      .WhenNullable(items =>
        new ExtractionOutcome
        {
          Device = plan.Device,
          Items = items
        });

  private ExtractionOutcomeItem CreateOutcomeItem(
      ExtractionPlan plan,
      ExtractionPlanItem item,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    new ExtractionOutcomeItem
    {
      Bucket = bucket,
      Original = item,
      Next = PlanNextExtraction(plan.Device, item, bucket)
    };

  private ExtractionPlanItem? PlanNextExtraction(
      ExtractionDevice device,
      ExtractionPlanItem last,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    ExtractionPlanItemCompleted(device, last, bucket) ? null
    : new ExtractionPlanItem
    {
      Due = last.Due + (last.Timeout * Math.Pow(2, last.Retries)),
      Retries = last.Retries + 1,
      Period = last.Period,
      Timeout = last.Timeout,
      ShouldValidate = last.ShouldValidate
    };

  private static bool ExtractionPlanItemCompleted(
      ExtractionDevice device,
      ExtractionPlanItem item,
      IExtractionBucket<ExtractionMeasurement> bucket) =>
    item.Retries >= device.ExtractionRetries ||
    bucket switch
    {
      (Period period,
       null,
       IEnumerable<ExtractionMeasurement> measurements) =>
        ExtractionPlanItemConsistent(device, item, measurements) &&
        item.ShouldValidate ?
          ExtractionPlanItemValid(device, item, measurements)
        : true,
      _ => false
    };

  private static bool ExtractionPlanItemConsistent(
      ExtractionDevice device,
      ExtractionPlanItem item,
      IEnumerable<ExtractionMeasurement> measurements) =>
    measurements.Count() >=
      item.Period.Span.TotalSeconds /
      device.MeasurementInterval.TotalSeconds;

  private static bool ExtractionPlanItemValid(
      ExtractionDevice device,
      ExtractionPlanItem item,
      IEnumerable<ExtractionMeasurement> measurements) =>
    measurements.All(ExtractionMeasurementExtensions.Validate);
}
