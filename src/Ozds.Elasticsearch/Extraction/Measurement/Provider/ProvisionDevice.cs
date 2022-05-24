namespace Ozds.Elasticsearch;

public readonly record struct ProvisionDevice
(string Id,
 string Source,
 string SourceDeviceId,
 ProvisionDeviceSourceData SourceDeviceData,
 TimeSpan MeasurementInterval,
 DateTime ExtractionStart);

public readonly record struct ProvisionDeviceSourceData
(string? OwnerId);

public static class ProvisionDeviceExtensions
{
  public static ProvisionDevice ToProvisionDevice(
      this Device device) =>
    new ProvisionDevice
    {
      Id = device.Id,
      Source = device.Source,
      SourceDeviceId = device.SourceDeviceId,
      SourceDeviceData =
        new ProvisionDeviceSourceData
        {
          OwnerId = device.SourceDeviceData.OwnerId
        },
      MeasurementInterval =
        TimeSpan.FromSeconds(device.MeasurementIntervalInSeconds),
      ExtractionStart = device.ExtractionStart,
    };

  public static ProvisionDevice ToProvisionDevice(
      this ExtractionDevice device) =>
    new ProvisionDevice
    {
      Id = device.Id,
      Source = device.Source,
      SourceDeviceId = device.SourceDeviceId,
      SourceDeviceData =
        new ProvisionDeviceSourceData
        {
          OwnerId = device.SourceDeviceData.OwnerId
        },
      MeasurementInterval = device.MeasurementInterval,
      ExtractionStart = device.ExtractionStart,
    };
}
