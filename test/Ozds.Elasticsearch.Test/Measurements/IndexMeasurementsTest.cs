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
    public void IndexMeasurementsTest()
    {
      var measurements =
          new List<Measurement> { Data.MyEnergyCommunityMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));

      var indexResponse = Client.IndexMeasurements(measurements);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(indexResponse.IsValid);

      var indexedMeasurementIds = indexResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

      foreach (var measurement in measurements)
      {
        var getResponse = Client.GetMeasurement(measurement.Id);
        Assert.True(getResponse.IsValid);

        var gotMeasurement = getResponse.Source;
        Assert.Equal(measurement, gotMeasurement);
      }

      var deleteResponse = Client.DeleteMeasurements(measurementIds);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(deleteResponse.IsValid);

      var deletedMeasurementIds = deleteResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }

    [Fact]
    public async Task IndexMeasurementsAsyncTest()
    {
      var measurements =
          new List<Measurement> { Data.MyEnergyCommunityMeasurement };
      var measurementIds = measurements.Select(d => new Id(d.Id));

      var indexResponse = await Client.IndexMeasurementsAsync(measurements);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(indexResponse.IsValid);

      var indexedMeasurementIds = indexResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, indexedMeasurementIds);

      foreach (var measurement in measurements)
      {
        var getResponse = await Client.GetMeasurementAsync(measurement.Id);
        Assert.True(getResponse.IsValid);

        var gotMeasurement = getResponse.Source;
        Assert.Equal(measurement, gotMeasurement);
      }

      var deleteResponse =
          await Client.DeleteMeasurementsAsync(measurementIds);
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
      // Assert.True(deleteResponse.IsValid);

      var deletedMeasurementIds = deleteResponse.Items.Ids();
      AssertExtensions.ElementsEqual(measurementIds, deletedMeasurementIds);
    }
  }
}
