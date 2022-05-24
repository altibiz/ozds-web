using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAwait(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);

  public Task<IEnumerable<ExtractionPlan>>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);

  public IEnumerable<ExtractionPlan>
  PlanSourceExtraction(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAwait(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    SearchDevicesBySourceAsync(source)
      .Then(devices => devices
        .Sources()
        .Select(device =>
          PlanDeviceExtractionAsync(
            device.ToExtractionDevice(),
            period,
            measurementsPerExtractionPlanItem))
        .ToAsync())
      .ToAsyncEnumerableNonNullable()
      .Flatten();

  public Task<IEnumerable<ExtractionPlan>>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    SearchDevicesBySourceAsync(source)
      .Then(devices => devices
        .Sources()
        .Select(device =>
          PlanDeviceExtractionAsync(
            device.ToExtractionDevice(),
            period,
            measurementsPerExtractionPlanItem))
        .Await())
      .FlattenTask();

  public IEnumerable<ExtractionPlan>
  PlanSourceExtraction(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    SearchDevicesBySource(source)
      .Sources()
      .Select(device =>
        PlanDeviceExtraction(
          device.ToExtractionDevice(),
          period,
          measurementsPerExtractionPlanItem));
}
