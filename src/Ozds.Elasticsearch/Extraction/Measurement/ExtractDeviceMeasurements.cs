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
            period),
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
            period),
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
            period),
        Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>());
}
