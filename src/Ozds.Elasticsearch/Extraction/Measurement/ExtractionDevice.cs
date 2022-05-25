namespace Ozds.Elasticsearch;

public readonly record struct ExtractionDevice
(string Id,
 string Source,
 string SourceDeviceId,
 ExtractionDeviceSourceData SourceDeviceData,
 TimeSpan MeasurementInterval,
 DateTime ExtractionStart,
 int ExtractionRetries,
 TimeSpan ExtractionOffset,
 TimeSpan ExtractionTimeout,
 TimeSpan ValidationInterval,
 DateTime LastValidation);

public readonly record struct ExtractionDeviceSourceData
(string? OwnerId);

public static class ExtractionDeviceExtensions
{
  public static ExtractionDevice ToExtractionDevice(
      this Device device) =>
    new ExtractionDevice
    {
      Id = device.Id,
      Source = device.Source,
      SourceDeviceId = device.SourceDeviceId,
      SourceDeviceData =
        new ExtractionDeviceSourceData
        {
          OwnerId = device.SourceDeviceData.OwnerId
        },
      MeasurementInterval =
        TimeSpan.FromSeconds(device.MeasurementIntervalInSeconds),
      ExtractionStart = device.ExtractionStart,
      ExtractionOffset =
        TimeSpan.FromSeconds(device.ExtractionOffsetInSeconds),
      ExtractionTimeout =
        TimeSpan.FromSeconds(device.ExtractionTimeoutInSeconds),
      ExtractionRetries = device.ExtractionRetries,
      ValidationInterval =
        TimeSpan.FromSeconds(device.ValidationIntervalInSeconds),
      LastValidation = device.LastValidation
    };
}
