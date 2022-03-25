using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Nest;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void SearchMeasurements() {
      var measurements = new List<Measurement> { Data.TestMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));
      var measurementPeriod = measurements.GetMeasurementPeriod();

          var indexResponse = _client.IndexMeasurements(measurements);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedMeasurementIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

          // NOTE: ES needs some time to prepare for searching
          System.Threading.Thread.Sleep(1000);
          var searchResponse = _client.SearchMeasurements(
              measurementPeriod.Min, measurementPeriod.Max);
          Assert.True(searchResponse.IsValid);

          var searchedMeasurements = searchResponse.Sources();
          AssertExtensions.Superset(measurements, searchedMeasurements);

          var deleteResponse = _client.DeleteMeasurements(measurementIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedMeasurementIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }

    [Fact]
    public async Task SearchMeasurementsAsync() {
      var measurements = new List<Measurement> { Data.TestMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));
      var measurementPeriod = measurements.GetMeasurementPeriod();

          var indexResponse =
              await _client.IndexMeasurementsAsync(measurements);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedMeasurementIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

          // NOTE: ES needs some time to prepare for searching
          System.Threading.Thread.Sleep(1000);
          var searchResponse = await _client.SearchMeasurementsAsync(
              measurementPeriod.Min, measurementPeriod.Max);
          Assert.True(searchResponse.IsValid);

          var searchedMeasurements = searchResponse.Sources();
          AssertExtensions.Superset(measurements, searchedMeasurements);

          var deleteResponse =
              await _client.DeleteMeasurementsAsync(measurementIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedMeasurementIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }
  }
}
