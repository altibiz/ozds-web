using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Nest;

namespace Ozds.Elasticsearch.Test
{
  public partial class ClientTest
  {
    [Fact]
    public void IndexDevicesTest()
    {
      var devices = new List<Device> { Data.MyEnergyCommunityDevice };
      var deviceIds = devices.Select(d => new Id(d.Id));

      var indexResponse = Client.IndexDevices(devices);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(indexResponse.IsValid);

      var indexedDeviceIds = indexResponse.Items.Ids();
      AssertExtensions.ElementsEqual(deviceIds, indexedDeviceIds);

      foreach (var device in devices)
      {
        var getResponse = Client.GetDevice(device.Id);
        Assert.True(getResponse.IsValid);

        var gotDevice = getResponse.Source;
        Assert.Equal(device, gotDevice);
      }

      var deleteResponse = Client.DeleteDevices(deviceIds);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(deleteResponse.IsValid);

      var deletedDeviceIds = deleteResponse.Items.Ids();
      AssertExtensions.ElementsEqual(deviceIds, deletedDeviceIds);
    }

    [Fact]
    public async Task IndexDevicesAsyncTest()
    {
      var devices = new List<Device> { Data.MyEnergyCommunityDevice };
      var deviceIds = devices.Select(d => new Id(d.Id));

      var indexResponse = await Client.IndexDevicesAsync(devices);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(indexResponse.IsValid);

      var indexedDeviceIds = indexResponse.Items.Ids();
      AssertExtensions.ElementsEqual(deviceIds, indexedDeviceIds);

      foreach (var device in devices)
      {
        var getResponse = await Client.GetDeviceAsync(device.Id);
        Assert.True(getResponse.IsValid);

        var gotDevice = getResponse.Source;
        Assert.Equal(device, gotDevice);
      }

      var deleteResponse = await Client.DeleteDevicesAsync(deviceIds);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(deleteResponse.IsValid);

      var deletedDeviceIds = deleteResponse.Items.Ids();
      AssertExtensions.ElementsEqual(deviceIds, deletedDeviceIds);
    }
  }
}
