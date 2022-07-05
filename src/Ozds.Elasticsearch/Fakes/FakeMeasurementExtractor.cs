using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public class FakeMeasurementExtractor : IMeasurementExtractor
{
  public Task<bool>
  CheckReadyAsync() => true.ToTask();

  public bool
  CheckReady() => true;

  public AsyncMeasurementExtraction
  ExecuteExtractionPlanAsync(ExtractionPlan plan) =>
    new AsyncMeasurementExtraction
    {
      Device = plan.Device,
      Items = AsyncEnumerable.Empty<MeasurementExtractionItem>()
    };

  public MeasurementExtraction
  ExecuteExtractionPlan(ExtractionPlan plan) =>
    new MeasurementExtraction
    {
      Device = plan.Device,
      Items = Enumerable.Empty<MeasurementExtractionItem>()
    };

  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    AsyncEnumerable.Empty<ExtractionPlan>();

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Enumerable.Empty<ExtractionPlan>();

  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    AsyncEnumerable.Empty<ExtractionPlan>();

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
    Enumerable.Empty<ExtractionPlan>();

  public Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    new ExtractionPlan
    {
      Device = device,
      Items = Enumerable.Empty<ExtractionPlanItem>()
    }
    .ToTask();

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    new ExtractionPlan
    {
      Device = device,
      Items = Enumerable.Empty<ExtractionPlanItem>()
    };

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurementsAsync(
      Period? period = null) =>
    AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurements(
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null) =>
    AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null) =>
    AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurements(
      ExtractionDevice device,
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();
}
