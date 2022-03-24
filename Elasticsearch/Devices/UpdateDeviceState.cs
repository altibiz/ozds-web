using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public UpdateResponse<Device> UpdateDeviceState(Id deviceId, string state);
  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId, string state);
};

public sealed partial class Client : IClient {
  public UpdateResponse<Device> UpdateDeviceState(Id deviceId, string state) =>
      this._client.Update<Device, DeviceStateUpdatePartial>(
          deviceId, d => d.Doc(new DeviceStateUpdatePartial(state)));

  public Task<UpdateResponse<Device>> UpdateDeviceStateAsync(
      Id deviceId, string state) =>
      this._client.UpdateAsync<Device, DeviceStateUpdatePartial>(
          deviceId, d => d.Doc(new DeviceStateUpdatePartial(state)));
}

internal class DeviceStateUpdatePartial {
  public DeviceStateUpdatePartial(string state) { this.state = state; }

  public string state { get; init; }
}
}
