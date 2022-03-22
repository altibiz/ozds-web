using System.Collections.Generic;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    // TODO: implement
    [Fact]
    public void AddMeasurements() {
      this._client.AddMeasurements(
          new List<Measurement> { new Measurement {} });
      var measurements = this._client.GetMeasurements();
      Assert.NotEmpty(measurements);
    }
  }
}
