namespace Ozds.Elasticsearch;

public interface IMeasurementExtractor
{

  public MeasurementExtractionAsync
  ExecuteExtractionPlanAsync(ExtractionPlan plan);

  public MeasurementExtraction
  ExecuteExtractionPlan(ExtractionPlan plan);

  // TODO: tweak
  public const int DefaultMeasurementsPerExtractionPlanItem =
    100;
  public const int DefaultMissingDataExtractionPlanItemsLimit =
    20;
  public const int DefaultLoadExtractionSpanLimitInSeconds =
    600;

  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAwait(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAwait(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public Task<IEnumerable<ExtractionPlan>>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public IEnumerable<ExtractionPlan>
  PlanSourceExtraction(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        DefaultLoadExtractionSpanLimitInSeconds);

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurementsAwait(
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractMeasurementsAsync(
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurements(
      Period? period = null);

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAwait(
      string source,
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null);

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurementsAwait(
      ExtractionDevice device,
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurements(
      ExtractionDevice device,
      Period? period = null);
}
