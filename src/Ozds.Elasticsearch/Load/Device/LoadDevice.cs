namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IDeviceLoader { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public Task LoadDeviceAsync(LoadDevice device) =>
    IndexDeviceAsync(
      new Device(
        source:
          device.source,
        sourceDeviceId:
          device.sourceDeviceId,
        sourceDeviceData:
          new Device.SourceDeviceDataType
          {
            OwnerId = device.sourceDeviceData?.ownerId
          },
        phase: device.phase,
        owner:
          new Device.OwnerDataType(
            @operator: device.owner.@operator,
            centerId: device.owner.centerId,
            centerUserId: device.owner.centerUserId,
            ownerId: device.owner.ownerId,
            ownerUserId: device.owner.ownerUserId),
        measurement:
          new Device.MeasurementDataType(
            measurementIntervalInSeconds:
              device.measurement.measurementIntervalInSeconds,
            extractionStart:
              device.measurement.extractionStart,
            extractionOffsetInSeconds:
              device.measurement.extractionOffsetInSeconds,
            extractionRetries:
              device.measurement.extractionRetries,
            extractionTimeoutInSeconds:
              device.measurement.extractionTimeoutInSeconds,
            validationIntervalInSeconds:
              device.measurement.validationIntervalInSeconds),
        state:
          new Device.StateDataType(
            device.state.state)));

  public void LoadDevice(LoadDevice device) =>
    IndexDevice(
      new Device(
        source:
          device.source,
        sourceDeviceId:
          device.sourceDeviceId,
        sourceDeviceData:
          new Device.SourceDeviceDataType
          {
            OwnerId = device.sourceDeviceData?.ownerId
          },
        phase: device.phase,
        owner:
          new Device.OwnerDataType(
            @operator: device.owner.@operator,
            centerId: device.owner.centerId,
            centerUserId: device.owner.centerUserId,
            ownerId: device.owner.ownerId,
            ownerUserId: device.owner.ownerUserId),
        measurement:
          new Device.MeasurementDataType(
            measurementIntervalInSeconds:
              device.measurement.measurementIntervalInSeconds,
            extractionStart:
              device.measurement.extractionStart,
            extractionOffsetInSeconds:
              device.measurement.extractionOffsetInSeconds,
            extractionRetries:
              device.measurement.extractionRetries,
            extractionTimeoutInSeconds:
              device.measurement.extractionTimeoutInSeconds,
            validationIntervalInSeconds:
              device.measurement.validationIntervalInSeconds),
        state:
          new Device.StateDataType(
            device.state.state)));
}
