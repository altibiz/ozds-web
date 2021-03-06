using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
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

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId,
      string state,
      DateTime? dateRemoved = null) =>
    Elastic.UpdateAsync<Device, DeviceStateUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceStateUpdatePartial(state, dateRemoved))
          .RefreshInDevelopment(Env)
          .Index(DeviceIndexName));

  public UpdateResponse<Device> UpdateDeviceState(
      Id deviceId,
      string state,
      DateTime? dateRemoved = null) =>
    Elastic.Update<Device, DeviceStateUpdatePartial>(
        deviceId,
        d => d
          .Doc(new DeviceStateUpdatePartial(state, dateRemoved))
          .RefreshInDevelopment(Env)
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

  [Object(Name = "state")]
  public StateDataPartial StateData { get; }

  internal class StateDataPartial
  {
    [Keyword(Name = "state")]
    public string State { get; init; } = default!;

    [Date(Name = "dateRemoved")]
    public DateTime? DateRemoved { get; init; } = default!;
  }
}
