using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Nest;

namespace Ozds.Elasticsearch.Test
{
  public partial class ClientTest
  {
    [Fact]
    public void SearchMeasurementsTest()
    {
      var measurements =
          new List<Measurement> { Data.MyEnergyCommunityMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));
      var measurementPeriod = measurements.GetLooseMeasurementPeriod();

      var indexResponse = Client.IndexMeasurements(measurements);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(indexResponse.IsValid);

      var indexedMeasurementIds = indexResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

      // NOTE: ES needs some time to prepare for searching
      System.Threading.Thread.Sleep(1000);
      var searchResponse = Client.SearchMeasurements(measurementPeriod);
      Assert.True(searchResponse.IsValid);

      var searchedMeasurements = searchResponse.Sources();
      AssertExtensions.Superset(measurements, searchedMeasurements);

      var deleteResponse = Client.DeleteMeasurements(measurementIds);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(deleteResponse.IsValid);

      var deletedMeasurementIds = deleteResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }

    [Fact]
    public async Task SearchMeasurementsAsyncTest()
    {
      var measurements =
          new List<Measurement> { Data.MyEnergyCommunityMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));
      var measurementPeriod = measurements.GetLooseMeasurementPeriod();

      var indexResponse = await Client.IndexMeasurementsAsync(measurements);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(indexResponse.IsValid);

      var indexedMeasurementIds = indexResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

      // NOTE: ES needs some time to prepare for searching
      System.Threading.Thread.Sleep(1000);
      var searchResponse =
          await Client.SearchMeasurementsAsync(measurementPeriod);
      Assert.True(searchResponse.IsValid);

      var searchedMeasurements = searchResponse.Sources();
      AssertExtensions.Superset(measurements, searchedMeasurements);

      var deleteResponse =
          await Client.DeleteMeasurementsAsync(measurementIds);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(deleteResponse.IsValid);

      var deletedMeasurementIds = deleteResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }
  }
}
