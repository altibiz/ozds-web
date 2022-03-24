using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void MeasurementProviderIterator() {
      Assert.NotEmpty(_measurementProviderIterator.Iterate());
    }
  }
}
