using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceLastValidationAsync(
      Id deviceId,
      DateTime lastValidation);

  public UpdateResponse<Device> UpdateDeviceLastValidation(
      Id deviceId,
      DateTime lastValidation);
};

public sealed partial class Client : IClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceLastValidationAsync(
      Id deviceId,
      DateTime lastValidation) =>
    Elasticsearch.UpdateAsync<Device, DeviceLastValidationUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceLastValidationUpdatePartial(lastValidation))
          .Index(DeviceIndexName));

  public UpdateResponse<Device> UpdateDeviceLastValidation(
      Id deviceId,
      DateTime lastValidation) =>
    Elasticsearch.Update<Device, DeviceLastValidationUpdatePartial>(
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

  public MeasurementDataPartial MeasurementData { get; }

  internal class MeasurementDataPartial
  {
    public DateTime LastValidation { get; init; } = default!;
  }
}
