using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurementsAwait(
      ExtractionDevice device,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == device.Source)
      .WhenNonNullFinally(
        provider => provider
          .GetMeasurementsAwait(
            device.ToProvisionDevice(),
            period)
          .ForEachAsync(bucket => Logger.LogDebug(
            $"Extracted {bucket.Count()} measurements at {period} " +
            $"for {device.Id} from {provider.Source}")),
        AsyncEnumerable.Empty<IExtractionBucket<ExtractionMeasurement>>());

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == device.Source)
      .WhenNonNullFinallyAsync(
        provider => provider
          .GetMeasurementsAsync(
            device.ToProvisionDevice(),
            period)
          .Then(buckets => buckets
            .ForEach(bucket => Logger.LogDebug(
              $"Extracted {bucket.Count()} measurements at {period} " +
              $"for {device.Id} from {provider.Source}"))),
        Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>());

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
