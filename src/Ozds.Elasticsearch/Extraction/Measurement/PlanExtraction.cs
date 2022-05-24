using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAwait(
      Period? period = null);

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null);

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanExtractionAwait(
      Period? period = null) =>
    Providers
      .Select(provider =>
        PlanSourceExtractionAwait(provider.Source, period))
      .Flatten();

  public Task<IEnumerable<ExtractionPlan>>
  PlanExtractionAsync(
      Period? period = null) =>
    Providers
      .Select(provider =>
        PlanSourceExtractionAsync(provider.Source, period))
      .Await()
      .Then(Enumerables.Flatten);

  public IEnumerable<ExtractionPlan>
  PlanExtraction(
      Period? period = null) =>
    Providers
      .Select(provider =>
        PlanSourceExtraction(provider.Source, period))
      .Flatten();
}
