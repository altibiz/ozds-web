using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Fact]
  public void UpdateDeviceLastValidationTest()
  {
    var device = Data.MyEnergyCommunityDevice;
    var deviceId = device.Id;
    var deviceState = device.State;
    var newDeviceLastValidation = DateTime.UtcNow;

    var indexResponse = Client.IndexDevice(device);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(deviceId, indexedId);

    var updateResponse = Client
      .UpdateDeviceLastValidation(deviceId, newDeviceLastValidation);
    Assert.True(updateResponse.IsValid);

    var updatedDeviceId = updateResponse.Id;
    Assert.Equal(deviceId, updatedDeviceId);

    var getResponse = Client.GetDevice(deviceId);
    Assert.True(getResponse.IsValid);

    var gotDevice = getResponse.Source;
    var gotDeviceId = gotDevice.Id;
    var gotDeviceLastValidation = gotDevice.LastValidation;
    Assert.Equal(deviceId, gotDeviceId);
    Assert.Equal(newDeviceLastValidation, gotDeviceLastValidation);

    var deleteResponse = Client.DeleteDevice(deviceId);
    Assert.True(deleteResponse.IsValid);

    var deletedDeviceId = deleteResponse.Id;
    Assert.Equal(deviceId, deletedDeviceId);
  }

  [Fact]
  public async Task UpdateDeviceLastValidationAsyncTest()
  {
    var device = Data.MyEnergyCommunityDevice;
    var deviceId = device.Id;
    var deviceState = device.State;
    var newDeviceLastValidation = DateTime.UtcNow;

    var indexResponse = await Client.IndexDeviceAsync(device);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(deviceId, indexedId);

    var updateResponse = await Client
      .UpdateDeviceLastValidationAsync(deviceId, newDeviceLastValidation);
    Assert.True(updateResponse.IsValid);

    var updatedDeviceId = updateResponse.Id;
    Assert.Equal(deviceId, updatedDeviceId);

    var getResponse = await Client.GetDeviceAsync(deviceId);
    Assert.True(getResponse.IsValid);

    var gotDevice = getResponse.Source;
    var gotDeviceId = gotDevice.Id;
    var gotDeviceLastValidation = gotDevice.LastValidation;
    Assert.Equal(deviceId, gotDeviceId);
    Assert.Equal(newDeviceLastValidation, gotDeviceLastValidation);

    var deleteResponse = await Client.DeleteDeviceAsync(deviceId);
    Assert.True(deleteResponse.IsValid);

    var deletedDeviceId = deleteResponse.Id;
    Assert.Equal(deviceId, deletedDeviceId);
  }
}
