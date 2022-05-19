namespace Ozds.Elasticsearch;

public partial interface IClient : IDeviceIndexer
{
}

public partial class Client : IClient
{
  public Task IndexDeviceAsync(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      decimal? measurementFrequency = null,
      string? state = null) =>
    IndexDeviceAsync(
      new Device(
        source,
        sourceDeviceId,
        new Device.KnownSourceDeviceData
        {
          ownerId = sourceDeviceData?.ownerId
        },
        measurementFrequency,
        state));


  public void IndexDevice(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      decimal? measurementFrequency = null,
      string? state = null) =>
    IndexDevice(
      new Device(
        source,
        sourceDeviceId,
        new Device.KnownSourceDeviceData
        {
          ownerId = sourceDeviceData?.ownerId
        },
        measurementFrequency,
        state));
}
