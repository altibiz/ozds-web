using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeMeasurementExtractor : IMeasurementExtractor
{
  public MeasurementExtractionAsync
  ExecuteExtractionPlanAsync(ExtractionPlan plan) =>
    new MeasurementExtractionAsync
    {
      Device = plan.Device,
      Items = Enumerables.EmptyAsync<MeasurementExtractionItem>()
    };

  public MeasurementExtraction
  ExecuteExtractionPlan(ExtractionPlan plan) =>
    new MeasurementExtraction
    {
      Device = plan.Device,
      Items = Enumerables.Empty<MeasurementExtractionItem>()
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
    Enumerables.EmptyAsync<ExtractionPlan>();

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Enumerables.Empty<ExtractionPlan>().ToTask();

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Enumerables.Empty<ExtractionPlan>();

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
    Enumerables.EmptyAsync<ExtractionPlan>();

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
    Enumerables.Empty<ExtractionPlan>().ToTask();

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
    Enumerables.Empty<ExtractionPlan>();

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
      Items = Enumerables.Empty<ExtractionPlanItem>()
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
      Items = Enumerables.Empty<ExtractionPlanItem>()
    };

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurementsAwait(
      Period? period = null) =>
    Enumerables.EmptyAsync<IExtractionBucket<ExtractionMeasurement>>();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractMeasurementsAsync(
      Period? period = null) =>
    Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>().ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurements(
      Period? period = null) =>
    Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAwait(
      string source,
      Period? period = null) =>
    Enumerables.EmptyAsync<IExtractionBucket<ExtractionMeasurement>>();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null) =>
    Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>().ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null) =>
    Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>();

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurementsAwait(
      ExtractionDevice device,
      Period? period = null) =>
    Enumerables.EmptyAsync<IExtractionBucket<ExtractionMeasurement>>();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null) =>
    Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>().ToTask();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurements(
      ExtractionDevice device,
      Period? period = null) =>
    Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>();
}
