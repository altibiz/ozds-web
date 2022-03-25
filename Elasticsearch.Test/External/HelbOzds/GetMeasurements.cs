using System.Threading.Tasks;
using System;
using Xunit;

namespace Elasticsearch.Test.HelbOzds {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var from = DateTime.Now.AddMinutes(-5);
      var to = DateTime.Now;

      var measurements = this._client.GetMeasurements(device, from, to);
      Assert.NotNull(measurements);
    }

    [Fact]
    public async Task GetMeasurementsAsync() {
      var device = Elasticsearch.Test.Data.TestDevice;
      var from = DateTime.Now.AddMinutes(-5);
      var to = DateTime.Now;

      var measurements =
          await this._client.GetMeasurementsAsync(device, from, to);
      Assert.NotNull(measurements);
    }
  }
}
