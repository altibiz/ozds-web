namespace Ozds.Elasticsearch;

public partial interface IClient : IDeviceLoader
{
}

public partial class Client : IClient
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
    IndexDeviceAsync(
      new Device(
        @operator,
        centerId,
        centerUserId,
        ownerId,
        ownerUserId,
        source,
        sourceDeviceId,
        measurementIntervalInSeconds,
        extractionStart,
        extractionOffsetInSeconds,
        extractionTimeoutInSeconds,
        extractionRetries,
        validationIntervalInSeconds,
        new Device.KnownSourceDeviceData
        {
          OwnerId = sourceDeviceData?.ownerId
        },
        state));


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
      string? state = null) =>
    IndexDevice(
      new Device(
        @operator,
        centerId,
        centerUserId,
        ownerId,
        ownerUserId,
        source,
        sourceDeviceId,
        measurementIntervalInSeconds,
        extractionStart,
        extractionOffsetInSeconds,
        extractionTimeoutInSeconds,
        extractionRetries,
        validationIntervalInSeconds,
        new Device.KnownSourceDeviceData
        {
          OwnerId = sourceDeviceData?.ownerId
        },
        state));
}
