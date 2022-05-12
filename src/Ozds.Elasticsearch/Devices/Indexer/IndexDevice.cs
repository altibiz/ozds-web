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
      string? state = null) =>
    IndexDeviceAsync(
      new Device(
        source,
        sourceDeviceId,
        new Device.KnownSourceDeviceData
        {
          ownerId = sourceDeviceData?.ownerId
        },
        state));


  public void IndexDevice(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null) =>
    IndexDevice(
      new Device(
        source,
        sourceDeviceId,
        new Device.KnownSourceDeviceData
        {
          ownerId = sourceDeviceData?.ownerId
        },
        state));
}
