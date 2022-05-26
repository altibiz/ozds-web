using Xunit;
using Ozds.Util;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void ExtractDeviceMeasurementsTest(IEnumerable<Device> devices)
  {
    foreach (var device in devices)
    {
      var buckets = Client
        .ExtractDeviceMeasurements(device.ToExtractionDevice());
      Logger.LogDebug(buckets.ToJson());
      Assert.NotEmpty(buckets);
      Assert.All(buckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
        }));
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task ExtractDeviceMeasurementsAsyncTest(
      IEnumerable<Device> devices)
  {
    foreach (var device in devices)
    {
      var buckets = await Client
        .ExtractDeviceMeasurementsAsync(device.ToExtractionDevice());
      Logger.LogDebug(buckets.ToJson());
      Assert.NotEmpty(buckets);
      Assert.All(buckets, bucket =>
        Assert.All(bucket, measurement =>
        {
          Assert.True(measurement.Validate());
        }));
    }
  }

  [Theory]
  [MemberData(
      nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
  public async Task ExtractDeviceMeasurementsInPeriodTest(
      IEnumerable<Device> devices, Period period)
  {
    foreach (var device in devices)
    {
      var buckets = await Client
        .ExtractDeviceMeasurementsAsync(
            device.ToExtractionDevice(),
            period);
      Logger.LogDebug(buckets.ToJson());
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
}
