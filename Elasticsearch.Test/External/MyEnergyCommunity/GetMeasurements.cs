using System;
using System.Linq;
using Xunit;

namespace Elasticsearch.Test.MyEnergyCommunity {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var period =
          new Period { From = DateTime.Now.AddMinutes(-5), To = DateTime.Now };

      var measurements = _client.GetMeasurements(device, period).ToList();
      Assert.True(measurements.Count > 5);
    }

    [Fact]
    public async void GetMeasurementsAsync() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var period =
          new Period { From = DateTime.Now.AddMinutes(-5), To = DateTime.Now };

      var measurements =
          (await _client.GetMeasurementsAsync(device, period)).ToList();
      Assert.True(measurements.Count > 5);
    }
  }
}
