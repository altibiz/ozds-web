using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Fact]
    public void IndexDeviceTest() {
      var device = Data.MyEnergyCommunityDevice;

      var indexResponse = Client.IndexDevice(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, device.Id);

      var getResponse = Client.GetDevice(device.Id);
      Assert.True(getResponse.IsValid);

      var gotDeviceId = getResponse.Source.Id;
      Assert.Equal(gotDeviceId, device.Id);

      var deleteResponse = Client.DeleteDevice(device.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deletedDeviceId, device.Id);
    }

    [Fact]
    public async Task IndexDeviceAsyncTest() {
      var device = Data.MyEnergyCommunityDevice;

      var indexResponse = await Client.IndexDeviceAsync(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, device.Id.ToString());

      var getResponse = await Client.GetDeviceAsync(device.Id);
      Assert.True(getResponse.IsValid);

      var gotDeviceId = getResponse.Source.Id;
      Assert.Equal(gotDeviceId, device.Id);

      var deleteResponse = await Client.DeleteDeviceAsync(device.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deletedDeviceId, device.Id.ToString());
    }
  }
}
