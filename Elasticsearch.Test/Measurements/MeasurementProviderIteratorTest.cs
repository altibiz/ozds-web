using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Fact]
    public void MeasurementProviderIteratorTest() {
      var measurementSources = MeasurementProviderIterator.Sources;
      AssertExtensions.Unique(measurementSources);
    }
  }
}
