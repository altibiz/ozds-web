namespace Ozds.Elasticsearch;

public class FakeDeviceLoader : IDeviceLoader
{
  public Task LoadDeviceAsync(
      string source,
      string sourceDeviceId,
      string phase,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state) => Task.CompletedTask;

  public void LoadDevice(
      string source,
      string sourceDeviceId,
      string phase,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state)
  { }
}
