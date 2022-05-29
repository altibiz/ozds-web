using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAwait(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Providers
      .Select(provider =>
        PlanSourceExtractionAwait(
          provider.Source,
          period,
          measurementsPerExtractionPlanItem,
          missingDataExtractionPlanItemsLimit,
          loadExtractionSpanLimitInSeconds))
      .Flatten();

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Providers
      .Select(provider =>
        PlanSourceExtractionAsync(
          provider.Source,
          period,
          measurementsPerExtractionPlanItem,
          missingDataExtractionPlanItemsLimit,
          loadExtractionSpanLimitInSeconds))
      .Await()
      .Then(Enumerables.Flatten);

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds) =>
    Providers
      .Select(provider =>
        PlanSourceExtraction(
          provider.Source,
          period,
          measurementsPerExtractionPlanItem,
          missingDataExtractionPlanItemsLimit,
          loadExtractionSpanLimitInSeconds))
      .Flatten();
}
