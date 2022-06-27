using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Fact]
  public void IndexDeviceTest()
  {
    var device = Data.MyEnergyCommunityDevice;

    var indexResponse = Client.IndexDevice(device);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(device.Id, indexedId);

    var getResponse = Client.GetDevice(device.Id);
    Assert.True(getResponse.IsValid);

    var gotDeviceId = getResponse.Source.Id;
    Assert.Equal(device.Id, gotDeviceId);

    var deleteResponse = Client.DeleteDevice(device.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedDeviceId = deleteResponse.Id;
    Assert.Equal(device.Id, deletedDeviceId);
  }

  [Fact]
  public async Task IndexDeviceAsyncTest()
  {
    var device = Data.MyEnergyCommunityDevice;

    var indexResponse = await Client.IndexDeviceAsync(device);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(device.Id, indexedId);

    var getResponse = await Client.GetDeviceAsync(device.Id);
    Assert.True(getResponse.IsValid);

    var gotDeviceId = getResponse.Source.Id;
    Assert.Equal(device.Id, gotDeviceId);

    var deleteResponse = await Client.DeleteDeviceAsync(device.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedDeviceId = deleteResponse.Id;
    Assert.Equal(device.Id, deletedDeviceId);
  }
}
