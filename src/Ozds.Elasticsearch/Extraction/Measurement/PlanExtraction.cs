using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAwait(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAwait(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    Providers
      .Select(provider =>
        PlanSourceExtractionAwait(
          provider.Source,
          period,
          measurementsPerExtractionPlanItem))
      .Flatten();

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    Providers
      .Select(provider =>
        PlanSourceExtractionAsync(
          provider.Source,
          period,
          measurementsPerExtractionPlanItem))
      .Await()
      .Then(Enumerables.Flatten);

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    Providers
      .Select(provider =>
        PlanSourceExtraction(
          provider.Source,
          period,
          measurementsPerExtractionPlanItem))
      .Flatten();
}
