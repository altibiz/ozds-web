using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAwait(
      string source,
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurementsAwait(
      string source,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == source)
      .WhenNonNullableFinallyTask(provider =>
        SearchDevicesBySourceAsync(source)
          .Then(devices => devices
            .Sources()
            .Select(device => provider
              .GetMeasurementsAwait(device.ToProvisionDevice()))
            .Flatten()))
      .ToAsyncEnumerable()
      .Flatten();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  ExtractSourceMeasurementsAsync(
      string source,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == source)
      .WhenNonNullableFinallyTask(provider =>
        SearchDevicesBySourceAsync(source)
          .Then(devices => devices
            .Sources()
            .Select(device => provider
              .GetMeasurementsAsync(device.ToProvisionDevice())))
          .ThenTask(Enumerables.Await)
          .Then(Enumerables.Flatten),
        Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  ExtractSourceMeasurements(
      string source,
      Period? period = null) =>
    Providers
      .Find(provider => provider.Source == source)
      .WhenNonNullableFinally(provider =>
        SearchDevicesBySource(source)
          .Sources()
          .Select(device => provider
            .GetMeasurements(device.ToProvisionDevice()))
          .Flatten(),
        Enumerables.Empty<IExtractionBucket<ExtractionMeasurement>>);
}
