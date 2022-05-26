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
    StateData =
      new StateDataPartial
      {
        State = state,
        DateRemoved = dateRemoved
      };
  }

  public StateDataPartial StateData { get; }

  internal class StateDataPartial
  {
    public string State { get; init; } = default!;
    public DateTime? DateRemoved { get; init; } = default!;
  }
}
