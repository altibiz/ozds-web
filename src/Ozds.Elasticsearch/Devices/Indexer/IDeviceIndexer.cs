namespace Ozds.Elasticsearch;

public readonly record struct SourceDeviceData
(string? ownerId);

public interface IDeviceIndexer
{
  public Task IndexDeviceAsync(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null);

  public void IndexDevice(
      string source,
      string sourceDeviceId,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null);
}
