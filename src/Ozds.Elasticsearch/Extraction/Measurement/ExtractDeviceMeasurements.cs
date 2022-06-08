using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == device.Source)
      .WhenNonNullFinally(
        provider => provider
          .GetMeasurementsAsync(
            device.ToProvisionDevice(),
            period)
          .ForEachAsync(bucket => Logger.LogDebug(
            $"Extracted {bucket.Count()} measurements at {period} " +
            $"for {device.Id} from {provider.Source}")),
        AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>());

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurements(
      ExtractionDevice device,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == device.Source)
      .WhenNonNullFinally(
        provider => provider
          .GetMeasurements(
            device.ToProvisionDevice(),
            period)
          .ForEach(bucket => Logger.LogDebug(
            $"Extracted {bucket.Count()} measurements at {period} " +
            $"for {device.Id} from {provider.Source}")),
        Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>());
}
