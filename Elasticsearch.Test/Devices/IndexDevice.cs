using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexDevice() {
      var device = Data.TestDevice;

      var id = _client.IndexDevice(device).Id;
      Assert.True(_client.GetDevice(id).IsValid);
    }
  }
}
