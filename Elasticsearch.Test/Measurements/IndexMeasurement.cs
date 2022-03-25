using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexMeasurement() {
      var measurement = Data.TestMeasurement;

      var indexResponse = _client.IndexMeasurement(measurement);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, measurement.Id);

      var getResponse = _client.GetMeasurement(measurement.Id);
      Assert.True(getResponse.IsValid);

      var gotMeasurementId = getResponse.Source.Id;
      Assert.Equal(gotMeasurementId, measurement.Id);

      var deleteResponse = _client.DeleteMeasurement(measurement.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedMeasurementId = deleteResponse.Id;
      Assert.Equal(deletedMeasurementId, measurement.Id);
    }

    [Fact]
    public async Task IndexMeasurementAsync() {
      var measurement = Data.TestMeasurement;

      var indexResponse = await _client.IndexMeasurementAsync(measurement);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, measurement.Id.ToString());

      var getResponse = await _client.GetMeasurementAsync(measurement.Id);
      Assert.True(getResponse.IsValid);

      var gotMeasurementId = getResponse.Source.Id;
      Assert.Equal(gotMeasurementId, measurement.Id);

      var deleteResponse = await _client.DeleteMeasurementAsync(measurement.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedMeasurementId = deleteResponse.Id;
      Assert.Equal(deletedMeasurementId, measurement.Id.ToString());
    }
  }
}
