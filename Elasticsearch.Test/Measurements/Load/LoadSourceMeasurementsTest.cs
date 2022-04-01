using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class ClientTest {
    [Theory]
    [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
    public void LoadSourceMeasurementsTest(IEnumerable<Device> devices) {
      var deviceIds = devices.Select(d => d.Id);
      SetupDevices(devices);

          // NOTE: preparation for searching
          Thread.Sleep(1000);
          var loadedMeasurements = Client.LoadSourceMeasurements(
              Elasticsearch.MeasurementFaker.Client.FakeSource);
          Assert.NotEmpty(loadedMeasurements); Assert.All(loadedMeasurements,
              m => AssertExtensions.OneOf(m.DeviceId, deviceIds));

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

    [Theory]
    [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
    public async Task LoadSourceMeasurementsAsyncTest(
        IEnumerable<Device> devices) {
      var deviceIds = devices.Select(d => d.Id);
      await SetupDevicesAsync(devices);

          // NOTE: preparation for searching
          Thread.Sleep(1000);
          var loadedMeasurements = await Client.LoadSourceMeasurementsAsync(
              Elasticsearch.MeasurementFaker.Client.FakeSource);
          Assert.NotEmpty(loadedMeasurements); Assert.All(loadedMeasurements,
              m => AssertExtensions.OneOf(m.DeviceId, deviceIds));

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

    [Theory]
    [MemberData(
        nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
    public async Task LoadSourceMeasurementsInPeriodTest(
        IEnumerable<Device> devices, Period period) {
      var deviceIds = devices.Select(d => d.Id);
      await SetupDevicesAsync(devices);

          // NOTE: preparation for searching
          Thread.Sleep(1000);
          var loadedMeasurements = await Client.LoadSourceMeasurementsAsync(
              Elasticsearch.MeasurementFaker.Client.FakeSource, period);
          Assert.NotEmpty(loadedMeasurements);
          Assert.All(loadedMeasurements, m => {
            Assert.InRange(m.MeasurementTimestamp, period.From, period.To);
            AssertExtensions.OneOf(m.DeviceId, deviceIds);
          });

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
