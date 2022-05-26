using Ozds.Util;

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
      .WhenNonNullableFinally(
        provider => provider
          .GetMeasurementsAwait(
            device.ToProvisionDevice(),
            period)
          .ForEach(bucket => Logger.LogDebug(
            $"Extracted {bucket.Count()} measurements at {period} " +
            $"for {device.Id} from {provider.Source}")),
        Enumerables.EmptyAsync<IExtractionBucket<ExtractionMeasurement>>());

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractDeviceMeasurementsAsync(
      ExtractionDevice device,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == device.Source)
      .WhenNonNullableFinallyTask(
        provider => provider
          .GetMeasurementsAsync(
            device.ToProvisionDevice(),
            period)
          .Then(buckets => buckets
            .ForEach(bucket => Logger.LogDebug(
              $"Extracted {bucket.Count()} measurements at {period} " +
              $"for {device.Id} from {provider.Source}"))),
        Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>());

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractDeviceMeasurements(
      ExtractionDevice device,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == device.Source)
      .WhenNonNullableFinally(
        provider => provider
          .GetMeasurements(
            device.ToProvisionDevice(),
            period)
          .ForEach(bucket => Logger.LogDebug(
            $"Extracted {bucket.Count()} measurements at {period} " +
            $"for {device.Id} from {provider.Source}")),
        Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>());
}
