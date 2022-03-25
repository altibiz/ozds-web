using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void UpdateDeviceState() {
      var device = Data.TestDevice;
      var deviceId = device.Id;
      var deviceState = device.State;
      var newDeviceState = deviceState == DeviceState.Healthy
                               ? DeviceState.Unhealthy
                               : DeviceState.Healthy;

      var indexResponse = _client.IndexDevice(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(deviceId, indexedId);

      var updateResponse = _client.UpdateDeviceState(deviceId, newDeviceState);
      Assert.True(updateResponse.IsValid);

      var updatedDeviceId = updateResponse.Id;
      Assert.Equal(deviceId, updatedDeviceId);

      var getResponse = _client.GetDevice(deviceId);
      Assert.True(getResponse.IsValid);

      var gotDevice = getResponse.Source;
      var gotDeviceId = gotDevice.Id;
      var gotDeviceState = gotDevice.State;
      Assert.Equal(deviceId, gotDeviceId);
      Assert.Equal(newDeviceState, gotDeviceState);

      var deleteResponse = _client.DeleteDevice(deviceId);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deviceId, deletedDeviceId);
    }

    [Fact]
    public async Task UpdateDeviceStateAsync() {
      var device = Data.TestDevice;
      var deviceId = device.Id;
      var deviceState = device.State;
      var newDeviceState = deviceState == DeviceState.Healthy
                               ? DeviceState.Unhealthy
                               : DeviceState.Healthy;

      var indexResponse = await _client.IndexDeviceAsync(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(deviceId, indexedId);

      var updateResponse =
          await _client.UpdateDeviceStateAsync(deviceId, newDeviceState);
      Assert.True(updateResponse.IsValid);

      var updatedDeviceId = updateResponse.Id;
      Assert.Equal(deviceId, updatedDeviceId);

      var getResponse = await _client.GetDeviceAsync(deviceId);
      Assert.True(getResponse.IsValid);

      var gotDevice = getResponse.Source;
      var gotDeviceId = gotDevice.Id;
      var gotDeviceState = gotDevice.State;
      Assert.Equal(deviceId, gotDeviceId);
      Assert.Equal(newDeviceState, gotDeviceState);

      var deleteResponse = await _client.DeleteDeviceAsync(deviceId);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deviceId, deletedDeviceId);
    }
  }
}
