namespace Ozds.Elasticsearch;

public readonly record struct SourceDeviceData
(string? ownerId);

public interface IDeviceLoader
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
      string? state = null);

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
      string? state = null);
}
