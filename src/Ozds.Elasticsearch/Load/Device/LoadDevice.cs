namespace Ozds.Elasticsearch;

public partial interface IClient : IDeviceLoader { }

public partial class Client : IClient
{
  public Task LoadDeviceAsync(
      string source,
      string sourceDeviceId,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state) =>
    IndexDeviceAsync(
      new Device(
        source,
        sourceDeviceId,
        new Device.SourceDeviceDataType
        {
          OwnerId = sourceDeviceData?.ownerId
        },
        new Device.OwnerDataType(
          owner.@operator,
          owner.centerId,
          owner.centerUserId,
          owner.ownerId,
          owner.ownerUserId),
        new Device.MeasurementDataType(
          measurement.measurementIntervalInSeconds,
          measurement.extractionStart,
          measurement.extractionOffsetInSeconds,
          measurement.extractionTimeoutInSeconds,
          measurement.extractionRetries,
          measurement.validationIntervalInSeconds),
        new Device.StateDataType(
          state.state)));

  public void LoadDevice(
      string source,
      string sourceDeviceId,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state) =>
    IndexDevice(
      new Device(
        source,
        sourceDeviceId,
        new Device.SourceDeviceDataType
        {
          OwnerId = sourceDeviceData?.ownerId
        },
        new Device.OwnerDataType(
          owner.@operator,
          owner.centerId,
          owner.centerUserId,
          owner.ownerId,
          owner.ownerUserId),
        new Device.MeasurementDataType(
          measurement.measurementIntervalInSeconds,
          measurement.extractionStart,
          measurement.extractionOffsetInSeconds,
          measurement.extractionTimeoutInSeconds,
          measurement.extractionRetries,
          measurement.validationIntervalInSeconds),
        new Device.StateDataType(
          state.state)));
}
