using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var measurements = this._client.GetMeasurements();
      Assert.NotNull(measurements);
    }
  }
}
