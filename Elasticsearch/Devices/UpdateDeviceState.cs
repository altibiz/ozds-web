using System;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public UpdateResponse<Device> UpdateDeviceState(
      Id deviceId, string state, DateTime? dateDiscontinued = null);
  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId, string state, DateTime? dateDiscontinued = null);
};

public sealed partial class Client : IClient {
  public UpdateResponse<Device> UpdateDeviceState(
      Id deviceId, string state, DateTime? dateDiscontinued = null) =>
      this._client.Update<Device, DeviceStateUpdatePartial>(deviceId,
          d => d.Doc(new DeviceStateUpdatePartial(state, dateDiscontinued)));

  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId, string state, DateTime? dateDiscontinued = null) =>
      this._client.UpdateAsync<Device, DeviceStateUpdatePartial>(deviceId,
          d => d.Doc(new DeviceStateUpdatePartial(state, dateDiscontinued)));
}

internal class DeviceStateUpdatePartial {
  public DeviceStateUpdatePartial(string state, DateTime? dateDiscontinued) {
    State = state;
    DateDiscontinued = dateDiscontinued;
  }

  public string State { get; init; }
  public DateTime? DateDiscontinued { get; init; }
}
}
