using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractMeasurementsAsync(
      Period? period = null) =>
    Providers
      .Select(provider =>
        ExtractSourceMeasurementsAsync(
          provider.Source,
          period))
      .FlattenAsync();

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
