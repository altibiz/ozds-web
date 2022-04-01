using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Fact]
    public void LoadSourceMeasurementsTest() {
      var device = Data.FakeDevice;

      var deviceIndexResponse = Client.IndexDevice(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = Client.GetDevice(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var loadedMeasurements = Client.LoadSourceMeasurements(
          Elasticsearch.MeasurementFaker.Client.FakeSource);
      Assert.NotEmpty(loadedMeasurements);
      Assert.All(loadedMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      // NOTE: preparation for searching
      Thread.Sleep(1000);
          var searchLogsResponse = Client.SearchLogs(LogType.LoadEnd);
          Assert.True(searchLogsResponse.IsValid);

          var logs = searchLogsResponse.Sources().ToList(); Assert.Single(logs);
          Assert.All(logs, l => {
            Assert.NotNull(l);
            Assert.NotNull(l.Data);
            Assert.NotNull(l.Data.Period);
            Assert.NotNull(l.Data.Period?.From);
            Assert.NotNull(l.Data.Period?.To);
          });
    }

    [Fact]
    public async Task LoadSourceMeasurementsAsyncTest() {
      var device = Data.FakeDevice;

      var deviceIndexResponse = await Client.IndexDeviceAsync(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = await Client.GetDeviceAsync(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var loadedMeasurements = await Client.LoadSourceMeasurementsAsync(
          Elasticsearch.MeasurementFaker.Client.FakeSource);
      Assert.NotEmpty(loadedMeasurements);
      Assert.All(loadedMeasurements, m => Assert.Equal(device.Id, m.DeviceId));

      // NOTE: preparation for searching
      Thread.Sleep(1000); var searchLoadLogsResponse =
                              await Client.SearchLogsAsync(LogType.LoadEnd);
          Assert.True(searchLoadLogsResponse.IsValid);

          var loadLogs = searchLoadLogsResponse.Sources().ToList();
          Assert.Single(loadLogs); Assert.All(loadLogs, l => {
            Assert.NotNull(l);
            Assert.NotNull(l.Data);
            Assert.NotNull(l.Data.Period);
            Assert.NotNull(l.Data.Period?.From);
            Assert.NotNull(l.Data.Period?.To);
          });
    }
  }
}
