using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public class FakeMeasurementExtractor : IMeasurementExtractor
{
  public MeasurementExtractionAsync
  ExecuteExtractionPlanAsync(ExtractionPlan plan) =>
    new MeasurementExtractionAsync
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
  PlanExtractionAwait(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    AsyncEnumerable.Empty<ExtractionPlan>();

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Enumerable.Empty<ExtractionPlan>().ToTask();

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
  PlanSourceExtractionAwait(
      string source,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    AsyncEnumerable.Empty<ExtractionPlan>();

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
    Enumerable.Empty<ExtractionPlan>().ToTask();

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
  ExtractMeasurementsAwait(
      Period? period = null) =>
    AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractMeasurementsAsync(
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>().ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurements(
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAwait(
      string source,
      Period? period = null) =>
    AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>().ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurementsAwait(
      ExtractionDevice device,
      Period? period = null) =>
    AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>().ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurements(
      ExtractionDevice device,
      Period? period = null) =>
    Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>();
}
