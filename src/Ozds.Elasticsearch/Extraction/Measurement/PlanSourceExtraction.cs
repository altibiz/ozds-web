using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAwait(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    SearchDevicesBySourceAsync(source)
      .Then(devices => devices
        .Sources()
        .Select(device =>
          PlanDeviceExtractionAsync(
            device.ToExtractionDevice(),
            period,
            measurementsPerExtractionPlanItem,
            missingDataExtractionPlanItemsLimit,
            loadExtractionSpanLimitInSeconds))
        .ToAsync())
      .ToAsyncEnumerable()
      .FlattenAsync();

  public Task<IEnumerable<ExtractionPlan>>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    SearchDevicesBySourceAsync(source)
      .Then(devices => devices
        .Sources()
        .Select(device =>
          PlanDeviceExtractionAsync(
            device.ToExtractionDevice(),
            period,
            measurementsPerExtractionPlanItem,
            missingDataExtractionPlanItemsLimit,
            loadExtractionSpanLimitInSeconds))
        .Await())
      .Flatten();

  public IEnumerable<ExtractionPlan>
  PlanSourceExtraction(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    SearchDevicesBySource(source)
      .Sources()
      .Select(device =>
        PlanDeviceExtraction(
          device.ToExtractionDevice(),
          period,
          measurementsPerExtractionPlanItem,
          missingDataExtractionPlanItemsLimit,
          loadExtractionSpanLimitInSeconds));
}
