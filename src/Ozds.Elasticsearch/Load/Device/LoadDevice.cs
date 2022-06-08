namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IDeviceLoader { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public Task LoadDeviceAsync(
      string source,
      string sourceDeviceId,
      string phase,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state) =>
    IndexDeviceAsync(
      new Device(
        source:
          source,
        sourceDeviceId:
          sourceDeviceId,
        sourceDeviceData:
          new Device.SourceDeviceDataType
          {
            OwnerId = sourceDeviceData?.ownerId
          },
        phase: phase,
        owner:
          new Device.OwnerDataType(
            @operator: owner.@operator,
            centerId: owner.centerId,
            centerUserId: owner.centerUserId,
            ownerId: owner.ownerId,
            ownerUserId: owner.ownerUserId),
        measurement:
          new Device.MeasurementDataType(
            measurementIntervalInSeconds:
              measurement.measurementIntervalInSeconds,
            extractionStart:
              measurement.extractionStart,
            extractionOffsetInSeconds:
              measurement.extractionOffsetInSeconds,
            extractionRetries:
              measurement.extractionRetries,
            extractionTimeoutInSeconds:
              measurement.extractionTimeoutInSeconds,
            validationIntervalInSeconds:
              measurement.validationIntervalInSeconds),
        state:
          new Device.StateDataType(
            state.state)));

  public void LoadDevice(
      string source,
      string sourceDeviceId,
      string phase,
      DeviceSourceDeviceData? sourceDeviceData,
      DeviceOwnerData owner,
      DeviceMeasurementData measurement,
      DeviceStateData state) =>
    IndexDevice(
      new Device(
        source:
          source,
        sourceDeviceId:
          sourceDeviceId,
        sourceDeviceData:
          new Device.SourceDeviceDataType
          {
            OwnerId = sourceDeviceData?.ownerId
          },
        phase: phase,
        owner:
          new Device.OwnerDataType(
            @operator: owner.@operator,
            centerId: owner.centerId,
            centerUserId: owner.centerUserId,
            ownerId: owner.ownerId,
            ownerUserId: owner.ownerUserId),
        measurement:
          new Device.MeasurementDataType(
            measurementIntervalInSeconds:
              measurement.measurementIntervalInSeconds,
            extractionStart:
              measurement.extractionStart,
            extractionOffsetInSeconds:
              measurement.extractionOffsetInSeconds,
            extractionRetries:
              measurement.extractionRetries,
            extractionTimeoutInSeconds:
              measurement.extractionTimeoutInSeconds,
            validationIntervalInSeconds:
              measurement.validationIntervalInSeconds),
        state:
          new Device.StateDataType(
            state.state)));
}
