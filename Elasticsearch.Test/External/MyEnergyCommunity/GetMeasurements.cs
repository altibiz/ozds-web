using System;
using System.Linq;
using Xunit;

namespace Elasticsearch.MyEnergyCommunity.Test {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var from = DateTime.Now.AddMinutes(-5);
      var to = DateTime.Now;

      var measurements = _client.GetMeasurements(device, from, to).ToList();
      Assert.True(measurements.Count > 5);
    }

    [Fact]
    public async void GetMeasurementsAsync() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var from = DateTime.Now.AddMinutes(-5);
      var to = DateTime.Now;

      var measurements =
          (await _client.GetMeasurementsAsync(device, from, to)).ToList();
      Assert.True(measurements.Count > 5);
    }
  }
}
