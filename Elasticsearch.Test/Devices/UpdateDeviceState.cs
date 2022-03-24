using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void UpdateDeviceState() {
      var device = Data.TestDevice;

      var id = _client.IndexDevice(device).Id;
      Assert.Equal(device.State, _client.GetDevice(id).Source.State);

      var newDeviceState = device.State == DeviceState.Healthy
                               ? DeviceState.Unhealthy
                               : DeviceState.Healthy;
      _client.UpdateDeviceState(id, newDeviceState);
      Assert.Equal(newDeviceState, _client.GetDevice(id).Source.State);
    }
  }
}
