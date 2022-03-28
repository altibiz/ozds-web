using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void LoadContinuously() {
      var device = Data.TestDevice;

      var deviceIndexResponse = _client.IndexDevice(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(indexedDeviceId, device.Id);

      var deviceGetResponse = _client.GetDevice(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(gotDevice, device);

      // NOTE: LoadContinuously searches for devices which takes some
      // NOTE: preparation from ES
      Thread.Sleep(1000);
      var firstLoadPeriod = new Period { From = DateTime.Now.AddMinutes(-10),
        To = DateTime.Now.AddMinutes(-5) };
      _client.LoadContinuously(_measurementProviderIterator, firstLoadPeriod);

      // NOTE: preparation for searching...
      Thread.Sleep(1000);
      var firstLoadSearchResponse = _client.SearchMeasurements(firstLoadPeriod);
      Assert.True(firstLoadSearchResponse.IsValid);

      var firstLoadMeasurements = firstLoadSearchResponse.Sources();
      Assert.NotEmpty(firstLoadMeasurements);
      Assert.All(
          firstLoadMeasurements, m => Assert.Equal(m.DeviceId, device.Id));

      var firstLoadMeasurementPeriod =
          firstLoadMeasurements.GetMeasurementPeriod();
          Assert.True(firstLoadMeasurementPeriod.From.ToUniversalTime() >=
                      firstLoadPeriod.From.ToUniversalTime());
          Assert.True(firstLoadMeasurementPeriod.To.ToUniversalTime() <=
                      firstLoadPeriod.To.ToUniversalTime());

          var secondLoadPeriod =
              new Period { From = firstLoadPeriod.To, To = DateTime.Now };
          // NOTE: not passing in the period this time because it should know
          // NOTE: by the last one
          _client.LoadContinuously(_measurementProviderIterator);

          // NOTE: preparation for searching...
          Thread.Sleep(1000); var secondLoadSearchResponse =
                                  _client.SearchMeasurements(secondLoadPeriod);
          Assert.True(secondLoadSearchResponse.IsValid);

          var secondLoadMeasurements = secondLoadSearchResponse.Sources();
          Assert.NotEmpty(secondLoadMeasurements); Assert.All(
              secondLoadMeasurements, m => Assert.Equal(m.DeviceId, device.Id));

      var secondLoadMeasurementPeriod =
          secondLoadMeasurements.GetMeasurementPeriod();
          Assert.True(secondLoadMeasurementPeriod.From.ToUniversalTime() >=
                      secondLoadPeriod.From.ToUniversalTime());
          Assert.True(secondLoadMeasurementPeriod.To.ToUniversalTime() <=
                      secondLoadPeriod.To.ToUniversalTime());

          var measurementIds =
              firstLoadMeasurements.Select(m => m.Id)
                  .Concat(secondLoadMeasurements.Select(m => m.Id))
                  .ToIds();
      var deleteMeasurementsResponse =
          _client.DeleteMeasurements(measurementIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteMeasurementsResponse.IsValid);

          var deletedMeasurementIds = deleteMeasurementsResponse.Items.Ids();
          Assert.Equal(deletedMeasurementIds, measurementIds);

          var deleteDeviceResponse = _client.DeleteDevice(device.Id);
          Assert.True(deleteDeviceResponse.IsValid);

          var deletedDeviceId = deleteDeviceResponse.Id;
          Assert.Equal(deletedDeviceId, device.Id);
    }

    [Fact]
    public async Task LoadContinuouslyAsync() {
      var device = Data.TestDevice;

      var deviceIndexResponse = await _client.IndexDeviceAsync(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(indexedDeviceId, device.Id);

      var deviceGetResponse = await _client.GetDeviceAsync(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(gotDevice, device);

      // NOTE: LoadContinuously searches for devices which takes some
      // NOTE: preparation from ES
      Thread.Sleep(1000);
      var firstLoadPeriod = (new Period { From = DateTime.Now.AddMinutes(-10),
        To = DateTime.Now.AddMinutes(-5) });
      await _client.LoadContinuouslyAsync(
          _measurementProviderIterator, firstLoadPeriod);

      // NOTE: preparation for searching...
      Thread.Sleep(1000);
      var firstLoadSearchResponse =
          await _client.SearchMeasurementsAsync(firstLoadPeriod);
      Assert.True(firstLoadSearchResponse.IsValid);

      var firstLoadMeasurements = firstLoadSearchResponse.Sources();
      Assert.NotEmpty(firstLoadMeasurements);
      Assert.All(
          firstLoadMeasurements, m => Assert.Equal(m.DeviceId, device.Id));

      var firstLoadMeasurementPeriod =
          firstLoadMeasurements.GetMeasurementPeriod();
          Assert.True(firstLoadMeasurementPeriod.From.ToUniversalTime() >=
                      firstLoadPeriod.From.ToUniversalTime());
          Assert.True(firstLoadMeasurementPeriod.To.ToUniversalTime() <=
                      firstLoadPeriod.To.ToUniversalTime());

          var secondLoadPeriod =
              (new Period { From = DateTime.Now.AddMinutes(-5),
                To = DateTime.Now });
          // NOTE: not passing in the period this time because it should know
          // NOTE: by the last one
          await _client.LoadContinuouslyAsync(_measurementProviderIterator);

          // NOTE: preparation for searching...
          Thread.Sleep(1000);
          var secondLoadSearchResponse =
              await _client.SearchMeasurementsAsync(secondLoadPeriod);
          Assert.True(secondLoadSearchResponse.IsValid);

          var secondLoadMeasurements = secondLoadSearchResponse.Sources();
          Assert.NotEmpty(secondLoadMeasurements); Assert.All(
              secondLoadMeasurements, m => Assert.Equal(m.DeviceId, device.Id));

      var secondLoadMeasurementPeriod =
          secondLoadMeasurements.GetMeasurementPeriod();
          Assert.True(secondLoadMeasurementPeriod.From.ToUniversalTime() >=
                      secondLoadPeriod.From.ToUniversalTime());
          Assert.True(secondLoadMeasurementPeriod.To.ToUniversalTime() <=
                      secondLoadPeriod.To.ToUniversalTime());

          var measurementIds =
              firstLoadMeasurements.Select(m => m.Id)
                  .Concat(secondLoadMeasurements.Select(m => m.Id))
                  .ToIds();
      var deleteMeasurementsResponse =
          _client.DeleteMeasurements(measurementIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteMeasurementsResponse.IsValid);

          var deletedMeasurementIds = deleteMeasurementsResponse.Items.Ids();
          Assert.Equal(deletedMeasurementIds, measurementIds);

          var deleteDeviceResponse = await _client.DeleteDeviceAsync(device.Id);
          Assert.True(deleteDeviceResponse.IsValid);

          var deletedDeviceId = deleteDeviceResponse.Id;
          Assert.Equal(deletedDeviceId, device.Id);
    }
  }
}
