namespace Ozds.Elasticsearch;

public class FakeDeviceLoader : IDeviceLoader
{
  public Task LoadDeviceAsync(
      string @operator,
      string centerId,
      string centerUserId,
      string ownerId,
      string ownerUserId,
      string source,
      string sourceDeviceId,
      int measurementIntervalInSeconds,
      DateTime extractionStart,
      int extractionOffsetInSeconds,
      int extractionTimeoutInSeconds,
      int extractionRetries,
      int validationIntervalInSeconds,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null) =>
    Task.CompletedTask;

  public void LoadDevice(
      string @operator,
      string centerId,
      string centerUserId,
      string ownerId,
      string ownerUserId,
      string source,
      string sourceDeviceId,
      int measurementIntervalInSeconds,
      DateTime extractionStart,
      int extractionOffsetInSeconds,
      int extractionTimeoutInSeconds,
      int extractionRetries,
      int validationIntervalInSeconds,
      SourceDeviceData? sourceDeviceData = null,
      string? state = null)
  { }
}
