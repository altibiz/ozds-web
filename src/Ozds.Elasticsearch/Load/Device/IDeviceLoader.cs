namespace Ozds.Elasticsearch;

public readonly record struct DeviceSourceDeviceData
(string? ownerId);

public readonly record struct DeviceOwnerData
(string @operator,
 string centerId,
 string? centerUserId,
 string ownerId,
 string? ownerUserId);

public readonly record struct DeviceMeasurementData
(int measurementIntervalInSeconds,
 DateTime extractionStart,
 int extractionOffsetInSeconds,
 int extractionTimeoutInSeconds,
 int extractionRetries,
 int validationIntervalInSeconds);

public readonly record struct DeviceStateData
(string? state = null);

public interface IDeviceLoader
{
  public Task LoadDeviceAsync(
      string source,
      string sourceDeviceId,
      string phase,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state);

  public void LoadDevice(
      string source,
      string sourceDeviceId,
      string phase,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state);
}
