using System.Collections.Generic;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexDevices() {
      var device = Data.TestDevice;

      _client.IndexDevices(new List<Device> { device });
      Assert.Contains(device, _client.SearchDevices(device.Source).Sources());
    }
  }
}
