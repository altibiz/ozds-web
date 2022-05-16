using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<IEnumerable<Measurement>> LoadDeviceMeasurementsAsync(
      Device device, Period? period = null);

  public IEnumerable<Measurement> LoadDeviceMeasurements(
      Device device, Period? period = null);
}

public partial class Client
{
  public Task<IEnumerable<Measurement>> LoadDeviceMeasurementsAsync(
      Device device, Period? period = null) =>
    Providers
      .Find(provider =>
        provider.Source == device.Source)
      .WhenNonNullableTask(provider =>
        provider
          .GetMeasurementsAsync(device, period)
          .Then(Enumerable.ToList)
          .ThenWith(measurements =>
            Logger.LogDebug(
              $"Fetched {measurements.Count} measurements " +
              $"from {device.Source} {device.SourceDeviceId} for {period}"))
          .Then(Enumerable.AsEnumerable)
          .Nullable(),
        Enumerable.Empty<Measurement>());

  public IEnumerable<Measurement> LoadDeviceMeasurements(
      Device device, Period? period = null) =>
    Providers
      .Find(provider =>
        provider.Source == device.Source)
      .WhenNonNullable(provider =>
        provider
          .GetMeasurements(device, period)
          .ToList()
          .WithNullable(measurements =>
            Logger.LogDebug(
              $"Fetched {measurements.Count} measurements " +
              $"from {device.Source} {device.SourceDeviceId} for {period}"))
          .AsEnumerable(),
        Enumerable.Empty<Measurement>());
}
