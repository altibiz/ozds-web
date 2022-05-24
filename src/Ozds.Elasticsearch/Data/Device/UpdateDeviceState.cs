using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId,
      string state,
      DateTime? dateRemoved = null);

  public UpdateResponse<Device> UpdateDeviceState(
      Id deviceId,
      string state,
      DateTime? dateRemoved = null);
};

public sealed partial class Client : IClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId,
      string state,
      DateTime? dateRemoved = null) =>
    Elasticsearch.UpdateAsync<Device, DeviceStateUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceStateUpdatePartial(state, dateRemoved))
          .Index(DeviceIndexName));

  public UpdateResponse<Device> UpdateDeviceState(
      Id deviceId,
      string state,
      DateTime? dateRemoved = null) =>
    Elasticsearch.Update<Device, DeviceStateUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceStateUpdatePartial(state, dateRemoved))
          .Index(DeviceIndexName));
}

internal class DeviceStateUpdatePartial
{
  public DeviceStateUpdatePartial(
      string state,
      DateTime? dateRemoved)
  {
    State = state;
    DateRemoved = dateRemoved;
  }

  public string State { get; init; }
  public DateTime? DateRemoved { get; init; }
}
