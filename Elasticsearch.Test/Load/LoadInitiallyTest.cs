using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Fact]
    public void LoadInitiallyTest() {
      var device = Data.FakeDevice;

      var deviceIndexResponse = Client.IndexDevice(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = Client.GetDevice(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);

      // NOTE: LoadInitially searches for devices which takes some
      // NOTE: preparation from ES
      Thread.Sleep(1000);
      Client.LoadInitially(Providers);

      // NOTE: preparation for searching...
      Thread.Sleep(1000);
      var initialLoadSearchMeasurementsResponse = Client.SearchMeasurements();
      Assert.True(initialLoadSearchMeasurementsResponse.IsValid);

      var initialLoadMeasurements =
          initialLoadSearchMeasurementsResponse.Sources();
      Assert.NotEmpty(initialLoadMeasurements);
      Assert.All(
          initialLoadMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      var initialLoadSearchLogsResponse = Client.SearchLogs(LogType.LoadEnd);
          Assert.True(initialLoadSearchLogsResponse.IsValid);

          var initialLoadLogs =
              initialLoadSearchLogsResponse.Sources().ToList();
          Assert.Single(initialLoadLogs); Assert.All(initialLoadLogs, log => {
            Assert.NotNull(log);
            Assert.NotNull(log.Data);
            Assert.NotNull(log.Data.Period);
            Assert.NotNull(log.Data.Period?.From);
            Assert.NotNull(log.Data.Period?.To);
          });
    }

    [Fact]
    public async Task LoadInitiallyAsyncTest() {
      var device = Data.FakeDevice;

      var deviceIndexResponse = await Client.IndexDeviceAsync(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = await Client.GetDeviceAsync(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);

      // NOTE: LoadInitially searches for devices which takes some
      // NOTE: preparation from ES
      Thread.Sleep(1000);
      await Client.LoadInitiallyAsync(Providers);

      // NOTE: preparation for searching...
      Thread.Sleep(1000);
      var initialLoadSearchResponse = await Client.SearchMeasurementsAsync();
      Assert.True(initialLoadSearchResponse.IsValid);

      var initialLoadMeasurements = initialLoadSearchResponse.Sources();
      Assert.NotEmpty(initialLoadMeasurements);
      Assert.All(
          initialLoadMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      var initialLoadSearchLogsResponse =
          await Client.SearchLogsAsync(LogType.LoadEnd);
          Assert.True(initialLoadSearchLogsResponse.IsValid);

          var initialLoadLogs =
              initialLoadSearchLogsResponse.Sources().ToList();
          Assert.Single(initialLoadLogs); Assert.All(initialLoadLogs, log => {
            Assert.NotNull(log);
            Assert.NotNull(log.Data);
            Assert.NotNull(log.Data.Period);
            Assert.NotNull(log.Data.Period?.From);
            Assert.NotNull(log.Data.Period?.To);
          });
    }
  }
}
