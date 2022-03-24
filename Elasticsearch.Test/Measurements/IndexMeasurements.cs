using System;
using System.Threading;
using System.Collections.Generic;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexMeasurements() {
      var measurement = Data.TestMeasurement;

      this._client.IndexMeasurements(new List<Measurement> { measurement });
      Thread.Sleep(1000);
      var measurements = this._client.SearchMeasurementsSorted(
          DateTime.Now.AddMinutes(-5), DateTime.Now);
      Assert.Contains(measurement, measurements.Sources());
    }
  }
}
