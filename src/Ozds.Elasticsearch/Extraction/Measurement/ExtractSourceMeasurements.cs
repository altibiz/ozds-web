using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == source)
      .WhenNonNullFinallyAsync(provider =>
        SearchDevicesBySourceAsync(source)
          .Then(devices => devices
            .Sources()
            .Select(device => provider
              .GetMeasurementsAsync(
                device.ToProvisionDevice(),
                period))
            .FlattenAsync()))
    .ToAsyncEnumerableNullable()
    .FlattenAsync();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == source)
      .WhenNonNullFinally(provider =>
        SearchDevicesBySource(source)
          .Sources()
          .Select(device => provider
            .GetMeasurements(
              device.ToProvisionDevice(),
              period))
          .Flatten(),
        Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>);
}
