using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeDeviceIndexer : IDeviceIndexer
{
  public Task IndexDeviceAsync(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null) =>
    Task.CompletedTask;


  public void IndexDevice(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null) =>
    IndexDeviceAsync(
      source,
      sourceDeviceId,
      sourceDeviceData,
      state).BlockTask();
}
