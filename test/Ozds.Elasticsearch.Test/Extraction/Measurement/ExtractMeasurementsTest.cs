using Xunit;
using Ozds.Extensions;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void ExtractMeasurementsTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id).ToList();
    SetupDevices(devices);

    var buckets = Client.ExtractMeasurements();
    Assert.NotEmpty(buckets);
    Assert.All(buckets, bucket =>
      Assert.All(bucket, measurement =>
      {
        Assert.True(measurement.Validate());
      }));
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task ExtractMeasurementsAsyncTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id).ToList();
    await SetupDevicesAsync(devices);

    var buckets = await Client.ExtractMeasurementsAsync().Await();
    Assert.NotEmpty(buckets);
    Assert.All(buckets, bucket =>
      Assert.All(bucket, measurement =>
      {
        Assert.True(measurement.Validate());
      }));
  }

  [Theory]
  [MemberData(
      nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
  public async Task ExtractMeasurementsInPeriodTest(
      IEnumerable<Device> devices, Period period)
  {
    var deviceIds = devices.Select(d => d.Id).ToList();
    await SetupDevicesAsync(devices);

    var buckets = await Client.ExtractMeasurementsAsync(period).Await();
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
}
