using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceLastValidationAsync(
      Id deviceId,
      DateTime lastValidation);

  public UpdateResponse<Device> UpdateDeviceLastValidation(
      Id deviceId,
      DateTime lastValidation);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceLastValidationAsync(
      Id deviceId,
      DateTime lastValidation) =>
    Elastic.UpdateAsync<Device, DeviceLastValidationUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceLastValidationUpdatePartial(lastValidation))
          .Index(DeviceIndexName));

  public UpdateResponse<Device> UpdateDeviceLastValidation(
      Id deviceId,
      DateTime lastValidation) =>
    Elastic.Update<Device, DeviceLastValidationUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceLastValidationUpdatePartial(lastValidation))
          .Index(DeviceIndexName));
}

internal class DeviceLastValidationUpdatePartial
{
  public DeviceLastValidationUpdatePartial(
      DateTime lastValidation)
  {
    MeasurementData =
      new MeasurementDataPartial
      {
        LastValidation = lastValidation
      };
  }

  [Object(Name = "measurement")]
  public MeasurementDataPartial MeasurementData { get; }

  internal class MeasurementDataPartial
  {
    [Date(Name = "lastValidation")]
    public DateTime LastValidation { get; init; } = default!;
  }
}
