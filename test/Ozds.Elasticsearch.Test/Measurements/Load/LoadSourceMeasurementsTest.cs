using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ozds.Elasticsearch.Test {
  public partial class ClientTest {
    [Theory]
    [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
    public void LoadSourceMeasurementsTest(IEnumerable<Device> devices) {
      var deviceIds = devices.Select(d => d.Id);
      SetupDevices(devices);

          // NOTE: preparation for searching
          Thread.Sleep(1000);
          var loadedMeasurements = Client.LoadSourceMeasurements(
              Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource);
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
              Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource);
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
              Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource, period);
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

    [Theory]
    [MemberData(
        nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
    public async Task LoadSourceMeasurementsConsecutivelyTest(
        IEnumerable<Device> devices, Period period) {
      var deviceIds = devices.Select(d => d.Id);
      await SetupDevicesAsync(devices);

          // NOTE: preparation for searching
          Thread.Sleep(1000);
          var firstLoadMeasurements = await Client.LoadSourceMeasurementsAsync(
              Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource, period);
          Assert.NotEmpty(firstLoadMeasurements);
          Assert.All(firstLoadMeasurements, m => {
            Assert.InRange(m.MeasurementTimestamp, period.From, period.To);
            AssertExtensions.OneOf(m.DeviceId, deviceIds);
          });

      // NOTE: preparation for searching
      Thread.Sleep(1000);
          var secondLoadMeasurements = await Client.LoadSourceMeasurementsAsync(
              Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource);
          Assert.NotEmpty(secondLoadMeasurements);
          Assert.All(secondLoadMeasurements, m => {
            Assert.True(m.MeasurementTimestamp >= period.To);
            AssertExtensions.OneOf(m.DeviceId, deviceIds);
          });

      // NOTE: preparation for searching
      Thread.Sleep(1000);
          var searchLogsResponse =
              await Client.SearchLogsSortedAsync(LogType.LoadEnd);
          Assert.True(searchLogsResponse.IsValid);

          var logs = searchLogsResponse.Sources().ToList();
          Assert.Equal(2, logs.Count);

          var firstLoadLog = logs[1]; var secondLoadLog = logs[0];
          Assert.NotNull(firstLoadLog.Data?.Period?.From);
          Assert.NotNull(firstLoadLog.Data?.Period?.To);
          Assert.NotNull(secondLoadLog.Data?.Period?.From);
          Assert.NotNull(secondLoadLog.Data?.Period?.To); Assert.Equal(
              firstLoadLog.Data?.Period?.To, secondLoadLog.Data?.Period?.From);
    }
  }
}
