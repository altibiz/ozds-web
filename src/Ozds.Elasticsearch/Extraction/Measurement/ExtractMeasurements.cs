using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurementsAwait(
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractMeasurementsAsync(
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurements(
      Period? period = null);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurementsAwait(
      Period? period = null) =>
    Providers
      .Select(provider =>
        ExtractSourceMeasurementsAwait(
          provider.Source,
          period))
      .Flatten();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractMeasurementsAsync(
      Period? period = null) =>
    Providers
      .Select(provider =>
        ExtractSourceMeasurementsAsync(
          provider.Source,
          period))
      .Await()
      .Then(Enumerables.Flatten);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurements(
      Period? period = null) =>
    Providers
      .Select(provider =>
        ExtractSourceMeasurements(
          provider.Source,
          period))
      .Flatten();
}
