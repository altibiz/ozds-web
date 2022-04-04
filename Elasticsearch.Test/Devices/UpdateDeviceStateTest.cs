using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test
{
  public partial class ClientTest
  {
    [Fact]
    public void UpdateDeviceStateTest()
    {
      var device = Data.MyEnergyCommunityDevice;
      var deviceId = device.Id;
      var deviceState = device.State;
      var newDeviceState = deviceState == DeviceState.Healthy
                               ? DeviceState.Unhealthy
                               : DeviceState.Healthy;

      var indexResponse = Client.IndexDevice(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(deviceId, indexedId);

      var updateResponse = Client.UpdateDeviceState(deviceId, newDeviceState);
      Assert.True(updateResponse.IsValid);

      var updatedDeviceId = updateResponse.Id;
      Assert.Equal(deviceId, updatedDeviceId);

      var getResponse = Client.GetDevice(deviceId);
      Assert.True(getResponse.IsValid);

      var gotDevice = getResponse.Source;
      var gotDeviceId = gotDevice.Id;
      var gotDeviceState = gotDevice.State;
      Assert.Equal(deviceId, gotDeviceId);
      Assert.Equal(newDeviceState, gotDeviceState);

      var deleteResponse = Client.DeleteDevice(deviceId);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deviceId, deletedDeviceId);
    }

    [Fact]
    public async Task UpdateDeviceStateAsyncTest()
    {
      var device = Data.MyEnergyCommunityDevice;
      var deviceId = device.Id;
      var deviceState = device.State;
      var newDeviceState = deviceState == DeviceState.Healthy
                               ? DeviceState.Unhealthy
                               : DeviceState.Healthy;

      var indexResponse = await Client.IndexDeviceAsync(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(deviceId, indexedId);

      var updateResponse =
          await Client.UpdateDeviceStateAsync(deviceId, newDeviceState);
      Assert.True(updateResponse.IsValid);

      var updatedDeviceId = updateResponse.Id;
      Assert.Equal(deviceId, updatedDeviceId);

      var getResponse = await Client.GetDeviceAsync(deviceId);
      Assert.True(getResponse.IsValid);

      var gotDevice = getResponse.Source;
      var gotDeviceId = gotDevice.Id;
      var gotDeviceState = gotDevice.State;
      Assert.Equal(deviceId, gotDeviceId);
      Assert.Equal(newDeviceState, gotDeviceState);

      var deleteResponse = await Client.DeleteDeviceAsync(deviceId);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deviceId, deletedDeviceId);
    }
  }
}
