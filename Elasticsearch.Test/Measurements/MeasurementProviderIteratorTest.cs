using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Fact]
    public void MeasurementProviderIteratorTest() {
      var measurementSources = Providers.Sources;
      AssertExtensions.Unique(measurementSources);
    }
  }
}
