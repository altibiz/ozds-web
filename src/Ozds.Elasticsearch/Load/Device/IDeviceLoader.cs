namespace Ozds.Elasticsearch;

public readonly record struct LoadDevice
(string source,
 string sourceDeviceId,
 string phase,
 LoadDeviceSourceDeviceData? sourceDeviceData,
 LoadDeviceOwnerData owner,
 LoadDeviceMeasurementData measurement,
 LoadDeviceStateData state);

public readonly record struct LoadDeviceSourceDeviceData
(string? ownerId);

public readonly record struct LoadDeviceOwnerData
(string @operator,
 string centerId,
 string? centerUserId,
 string ownerId,
 string? ownerUserId);

public readonly record struct LoadDeviceMeasurementData
(int measurementIntervalInSeconds,
 DateTime extractionStart,
 int extractionOffsetInSeconds,
 int extractionTimeoutInSeconds,
 int extractionRetries,
 int validationIntervalInSeconds);

public readonly record struct LoadDeviceStateData
(string? state = null);

public interface IDeviceLoader
{
  public Task LoadDeviceAsync(LoadDevice device);

  public void LoadDevice(LoadDevice device);
}
