using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void MeasurementProviderIterator() {
      var measurementSources = _measurementProviderIterator.Sources;
      AssertExtensions.Unique(measurementSources);
    }
  }
}
