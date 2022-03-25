using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Nest;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexMeasurements() {
      var measurements = new List<Measurement> { Data.TestMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));

      var indexResponse = _client.IndexMeasurements(measurements);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedMeasurementIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

          foreach (var measurement in measurements) {
            var getResponse = _client.GetMeasurement(measurement.Id);
            Assert.True(getResponse.IsValid);

            var gotMeasurement = getResponse.Source;
            Assert.Equal(measurement, gotMeasurement);
          }

          var deleteResponse = _client.DeleteMeasurements(measurementIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedMeasurementIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }

    [Fact]
    public async Task IndexMeasurementsAsync() {
      var measurements = new List<Measurement> { Data.TestMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));

      var indexResponse = await _client.IndexMeasurementsAsync(measurements);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedMeasurementIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

          foreach (var measurement in measurements) {
            var getResponse = await _client.GetMeasurementAsync(measurement.Id);
            Assert.True(getResponse.IsValid);

            var gotMeasurement = getResponse.Source;
            Assert.Equal(measurement, gotMeasurement);
          }

          var deleteResponse =
              await _client.DeleteMeasurementsAsync(measurementIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedMeasurementIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }
  }
}
