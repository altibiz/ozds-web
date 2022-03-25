using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Nest;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexDevices() {
      var devices = new List<Device> { Data.TestDevice };
      var deviceIds = devices.Select(d => new Id(d.Id));

      var indexResponse = _client.IndexDevices(devices);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedDeviceIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(deviceIds, indexedDeviceIds);

          foreach (var device in devices) {
            var getResponse = _client.GetDevice(device.Id);
            Assert.True(getResponse.IsValid);

            var gotDevice = getResponse.Source;
            Assert.Equal(device, gotDevice);
          }

          var deleteResponse = _client.DeleteDevices(deviceIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedDeviceIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(deviceIds, deletedDeviceIds);
    }

    [Fact]
    public async Task IndexDevicesAsync() {
      var devices = new List<Device> { Data.TestDevice };
      var deviceIds = devices.Select(d => new Id(d.Id));

      var indexResponse = await _client.IndexDevicesAsync(devices);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedDeviceIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(deviceIds, indexedDeviceIds);

          foreach (var device in devices) {
            var getResponse = await _client.GetDeviceAsync(device.Id);
            Assert.True(getResponse.IsValid);

            var gotDevice = getResponse.Source;
            Assert.Equal(device, gotDevice);
          }

          var deleteResponse = await _client.DeleteDevicesAsync(deviceIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedDeviceIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(deviceIds, deletedDeviceIds);
    }
  }
}
