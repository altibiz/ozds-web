using Ozds.Extensions;

namespace Ozds.Elasticsearch;

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
              .GetMeasurementsAwait(
                device.ToProvisionDevice(),
                period))
            .FlattenAsync()))
    .ToAsyncEnumerableNullable()
    .FlattenAsync();

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
              .GetMeasurementsAsync(
                device.ToProvisionDevice(),
                period)))
          .ThenTask(Enumerables.Await)
          .Then(Enumerables.Flatten),
        Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>);

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
            .GetMeasurements(
              device.ToProvisionDevice(),
              period))
          .Flatten(),
        Enumerable.Empty<IExtractionBucket<ExtractionMeasurement>>);
}
