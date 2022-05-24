using Xunit;

namespace Ozds.Elasticsearch.Test
{
  public partial class ClientTest
  {
    [Theory]
    [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
    public void LoadMeasurementsTest(IEnumerable<Device> devices)
    {
      var deviceIds = devices.Select(d => d.Id).ToList();
      SetupDevices(devices);

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var buckets = Client.ExtractMeasurements();
      Assert.NotEmpty(buckets);
      Assert.All(buckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
        }));

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var searchLogsResponse = Client.SearchLogs(LogType.LoadEnd);
      Assert.True(searchLogsResponse.IsValid);

      var logs = searchLogsResponse.Sources().ToList(); Assert.Single(logs);
      Assert.All(logs, l =>
      {
        Assert.NotNull(l);
        Assert.NotNull(l.Data);
        Assert.NotNull(l.Data.Period);
        Assert.NotNull(l.Data.Period?.From);
        Assert.NotNull(l.Data.Period?.To);
      });
    }

    [Theory]
    [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
    public async Task LoadMeasurementsAsyncTest(IEnumerable<Device> devices)
    {
      var deviceIds = devices.Select(d => d.Id).ToList();
      await SetupDevicesAsync(devices);

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var buckets = await Client.ExtractMeasurementsAsync();
      Assert.NotEmpty(buckets);
      Assert.All(buckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
        }));

      // NOTE: preparation for searching
      Thread.Sleep(1000); var searchLoadLogsResponse =
                              await Client.SearchLogsAsync(LogType.LoadEnd);
      Assert.True(searchLoadLogsResponse.IsValid);

      var loadLogs = searchLoadLogsResponse.Sources().ToList();
      Assert.Single(loadLogs); Assert.All(loadLogs, l =>
      {
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
    public async Task LoadMeasurementsInPeriodTest(
        IEnumerable<Device> devices, Period period)
    {
      var deviceIds = devices.Select(d => d.Id).ToList();
      await SetupDevicesAsync(devices);

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var buckets = await Client.ExtractMeasurementsAsync(period);
      Assert.NotEmpty(buckets);
      Assert.All(buckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
          Assert.InRange(
              measurement.Timestamp,
              period.From,
              period.To);
        }));
    }

    [Theory]
    [MemberData(
        nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
    public async Task LoadMeasurementsConsecutivelyTest(
        IEnumerable<Device> devices, Period period)
    {
      var deviceIds = devices.Select(d => d.Id);
      await SetupDevicesAsync(devices);

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var firstLoadBuckets = await Client
        .ExtractMeasurementsAsync(period);
      Assert.NotEmpty(firstLoadBuckets);
      Assert.All(firstLoadBuckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
          Assert.InRange(
              measurement.Timestamp,
              period.From,
              period.To);
        }));

      // NOTE: preparation for searching
      Thread.Sleep(1000);
      var secondLoadBuckets = await Client.ExtractMeasurementsAsync();
      Assert.NotEmpty(secondLoadBuckets);
      Assert.All(secondLoadBuckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
          Assert.InRange(
              measurement.Timestamp,
              period.From,
              period.To);
        }));
    }
  }
}
