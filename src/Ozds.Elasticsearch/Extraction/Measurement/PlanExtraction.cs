using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    SearchDevicesAsync()
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

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    SearchDevices()
      .Sources()
      .Select(device =>
        PlanDeviceExtraction(
          device.ToExtractionDevice(),
          period,
          measurementsPerExtractionPlanItem,
          missingDataExtractionPlanItemsLimit,
          loadExtractionSpanLimitInSeconds));
}
