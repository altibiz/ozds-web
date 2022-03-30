using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Fact]
    public void LoadContinuouslyTest() {
      var device = Data.FakeDevice;

      var deviceIndexResponse = Client.IndexDevice(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = Client.GetDevice(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);

      // NOTE: LoadContinuously searches for devices which takes some
      // NOTE: preparation from ES
      Thread.Sleep(1000);
      var firstLoadPeriod = new Period { From = DateTime.UtcNow.AddMinutes(-10),
        To = DateTime.UtcNow.AddMinutes(-5) };
      Client.LoadContinuously(Providers, firstLoadPeriod);

      // NOTE: preparation for searching...
      Thread.Sleep(1000);
      var firstLoadSearchResponse = Client.SearchMeasurements(firstLoadPeriod);
      Assert.True(firstLoadSearchResponse.IsValid);

      var firstLoadMeasurements = firstLoadSearchResponse.Sources();
      Assert.NotEmpty(firstLoadMeasurements);
      Assert.All(
          firstLoadMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      var firstLoadMeasurementPeriod =
          firstLoadMeasurements.GetMeasurementPeriod();
          Assert.True(firstLoadMeasurementPeriod.From.ToUniversalTime() >=
                      firstLoadPeriod.From.ToUniversalTime());
          Assert.True(firstLoadMeasurementPeriod.To.ToUniversalTime() <=
                      firstLoadPeriod.To.ToUniversalTime());

          var secondLoadPeriod =
              new Period { From = firstLoadPeriod.To, To = DateTime.UtcNow };
          // NOTE: not passing in the period this time because it should know
          // NOTE: by the last one
          Client.LoadContinuously(Providers);

          // NOTE: preparation for searching...
          Thread.Sleep(1000); var secondLoadSearchResponse =
                                  Client.SearchMeasurements(secondLoadPeriod);
          Assert.True(secondLoadSearchResponse.IsValid);

          var secondLoadMeasurements = secondLoadSearchResponse.Sources();
          Assert.NotEmpty(secondLoadMeasurements); Assert.All(
              secondLoadMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      var secondLoadMeasurementPeriod =
          secondLoadMeasurements.GetMeasurementPeriod();
          Assert.True(secondLoadMeasurementPeriod.From.ToUniversalTime() >=
                      secondLoadPeriod.From.ToUniversalTime());
          Assert.True(secondLoadMeasurementPeriod.To.ToUniversalTime() <=
                      secondLoadPeriod.To.ToUniversalTime());

          var continuousLoadLogSearchResponse =
              Client.SearchLogs(LogType.LoadEnd);
          Assert.True(continuousLoadLogSearchResponse.IsValid);

          var continuousLoadLogs =
              continuousLoadLogSearchResponse.Sources().ToList();
          Assert.Equal(2, continuousLoadLogs.Count);

          Assert.All(continuousLoadLogs, log => {
            Assert.Equal(log.Type, LogType.LoadEnd);
            Assert.NotNull(log.Data);
            Assert.NotNull(log.Data.Period);
            Assert.NotNull(log.Data.Period?.From);
            Assert.NotNull(log.Data.Period?.To);
          });
    }

    [Fact]
    public async Task LoadContinuouslyAsyncTest() {
      var device = Data.FakeDevice;

      var deviceIndexResponse = await Client.IndexDeviceAsync(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = await Client.GetDeviceAsync(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);

      // NOTE: LoadContinuously searches for devices which takes some
      // NOTE: preparation from ES
      Thread.Sleep(1000);
      var firstLoadPeriod =
          (new Period { From = DateTime.UtcNow.AddMinutes(-10),
            To = DateTime.UtcNow.AddMinutes(-5) });
      await Client.LoadContinuouslyAsync(Providers, firstLoadPeriod);

      // NOTE: preparation for searching...
      Thread.Sleep(1000);
      var firstLoadSearchResponse =
          await Client.SearchMeasurementsAsync(firstLoadPeriod);
      Assert.True(firstLoadSearchResponse.IsValid);

      var firstLoadMeasurements = firstLoadSearchResponse.Sources();
      Assert.NotEmpty(firstLoadMeasurements);
      Assert.All(
          firstLoadMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      var firstLoadMeasurementPeriod =
          firstLoadMeasurements.GetMeasurementPeriod();
          Assert.True(firstLoadMeasurementPeriod.From.ToUniversalTime() >=
                      firstLoadPeriod.From.ToUniversalTime());
          Assert.True(firstLoadMeasurementPeriod.To.ToUniversalTime() <=
                      firstLoadPeriod.To.ToUniversalTime());

          var secondLoadPeriod =
              (new Period { From = DateTime.UtcNow.AddMinutes(-5),
                To = DateTime.UtcNow });
          // NOTE: not passing in the period this time because it should know
          // NOTE: by the last one
          await Client.LoadContinuouslyAsync(Providers);

          // NOTE: preparation for searching...
          Thread.Sleep(1000);
          var secondLoadSearchResponse =
              await Client.SearchMeasurementsAsync(secondLoadPeriod);
          Assert.True(secondLoadSearchResponse.IsValid);

          var secondLoadMeasurements = secondLoadSearchResponse.Sources();
          Assert.NotEmpty(secondLoadMeasurements); Assert.All(
              secondLoadMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      var secondLoadMeasurementPeriod =
          secondLoadMeasurements.GetMeasurementPeriod();
          Assert.True(secondLoadMeasurementPeriod.From.ToUniversalTime() >=
                      secondLoadPeriod.From.ToUniversalTime());
          Assert.True(secondLoadMeasurementPeriod.To.ToUniversalTime() <=
                      secondLoadPeriod.To.ToUniversalTime());

          var continuousLoadLogSearchResponse =
              await Client.SearchLogsAsync(LogType.LoadEnd);
          Assert.True(continuousLoadLogSearchResponse.IsValid);

          var continuousLoadLogs =
              continuousLoadLogSearchResponse.Sources().ToList();
          Assert.Equal(2, continuousLoadLogs.Count);
          Assert.All(continuousLoadLogs, log => {
            Assert.Equal(log.Type, LogType.LoadEnd);
            Assert.NotNull(log.Data);
            Assert.NotNull(log.Data.Period);
            Assert.NotNull(log.Data.Period?.From);
            Assert.NotNull(log.Data.Period?.To);
          });
    }
  }
}
