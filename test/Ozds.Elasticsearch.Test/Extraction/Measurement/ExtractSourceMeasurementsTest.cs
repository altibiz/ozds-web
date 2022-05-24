using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void ExtractSourceMeasurementsTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    SetupDevices(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var buckets = Client
      .ExtractSourceMeasurements(
          Elasticsearch.MeasurementFaker.Client.FakeSource);
    Assert.NotEmpty(buckets);
    Assert.All(buckets, bucket =>
      Assert.All(bucket, measurement =>
      {
        Assert.True(measurement.Validate());
      }));
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task ExtractSourceMeasurementsAsyncTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    await SetupDevicesAsync(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var buckets = await Client
      .ExtractSourceMeasurementsAsync(
        Elasticsearch.MeasurementFaker.Client.FakeSource);
    Assert.All(buckets, bucket =>
      Assert.All(bucket, measurement =>
      {
        Assert.True(measurement.Validate());
      }));
  }

  [Theory]
  [MemberData(
      nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
  public async Task ExtractSourceMeasurementsInPeriodTest(
      IEnumerable<Device> devices, Period period)
  {
    var deviceIds = devices.Select(d => d.Id);
    await SetupDevicesAsync(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var buckets = await Client
      .ExtractSourceMeasurementsAsync(
        Elasticsearch.MeasurementFaker.Client.FakeSource, period);
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
}
