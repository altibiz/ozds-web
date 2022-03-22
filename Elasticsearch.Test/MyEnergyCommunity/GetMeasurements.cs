using Xunit;

namespace Elasticsearch.MyEnergyCommunity.Test {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var measurements = this._client.GetMeasurements("test-owner", "M9EQCU59");
      Assert.NotNull(measurements);
    }
  }
}
