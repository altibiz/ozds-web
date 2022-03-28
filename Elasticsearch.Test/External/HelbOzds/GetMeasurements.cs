using System.Threading.Tasks;
using System;
using Xunit;

namespace Elasticsearch.Test.HelbOzds {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var period =
          new Period { From = DateTime.Now.AddMinutes(-5), To = DateTime.Now };

      var measurements = this._client.GetMeasurements(device, period);
      Assert.NotNull(measurements);
    }

    [Fact]
    public async Task GetMeasurementsAsync() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var period =
          new Period { From = DateTime.Now.AddMinutes(-5), To = DateTime.Now };

      var measurements =
          await this._client.GetMeasurementsAsync(device, period);
      Assert.NotNull(measurements);
    }
  }
}
