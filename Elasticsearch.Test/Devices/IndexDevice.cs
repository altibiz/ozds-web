using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexDevice() {
      var device = Data.TestDevice;

      var indexResponse = _client.IndexDevice(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, device.Id);

      var getResponse = _client.GetDevice(device.Id);
      Assert.True(getResponse.IsValid);

      var gotDeviceId = getResponse.Source.Id;
      Assert.Equal(gotDeviceId, device.Id);

      var deleteResponse = _client.DeleteDevice(device.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deletedDeviceId, device.Id);
    }

    [Fact]
    public async Task IndexDeviceAsync() {
      var device = Data.TestDevice;

      var indexResponse = await _client.IndexDeviceAsync(device);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, device.Id.ToString());

      var getResponse = await _client.GetDeviceAsync(device.Id);
      Assert.True(getResponse.IsValid);

      var gotDeviceId = getResponse.Source.Id;
      Assert.Equal(gotDeviceId, device.Id);

      var deleteResponse = await _client.DeleteDeviceAsync(device.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedDeviceId = deleteResponse.Id;
      Assert.Equal(deletedDeviceId, device.Id.ToString());
    }
  }
}
