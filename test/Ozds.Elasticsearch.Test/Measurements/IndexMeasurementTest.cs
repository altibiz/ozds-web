using System.Threading.Tasks;
using Xunit;

namespace Ozds.Elasticsearch.Test
{
  public partial class ClientTest
  {
    [Fact]
    public void IndexMeasurementTest()
    {
      var measurement = Data.MyEnergyCommunityMeasurement;

      var indexResponse = Client.IndexMeasurement(measurement);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, measurement.Id);

      var getResponse = Client.GetMeasurement(measurement.Id);
      Assert.True(getResponse.IsValid);

      var gotMeasurementId = getResponse.Source.Id;
      Assert.Equal(gotMeasurementId, measurement.Id);

      var deleteResponse = Client.DeleteMeasurement(measurement.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedMeasurementId = deleteResponse.Id;
      Assert.Equal(deletedMeasurementId, measurement.Id);
    }

    [Fact]
    public async Task IndexMeasurementAsyncTest()
    {
      var measurement = Data.MyEnergyCommunityMeasurement;

      var indexResponse = await Client.IndexMeasurementAsync(measurement);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, measurement.Id.ToString());

      var getResponse = await Client.GetMeasurementAsync(measurement.Id);
      Assert.True(getResponse.IsValid);

      var gotMeasurementId = getResponse.Source.Id;
      Assert.Equal(gotMeasurementId, measurement.Id);

      var deleteResponse = await Client.DeleteMeasurementAsync(measurement.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedMeasurementId = deleteResponse.Id;
      Assert.Equal(deletedMeasurementId, measurement.Id.ToString());
    }
  }
}
