using Xunit;
using Ozds.Extensions;

namespace Ozds.Elasticsearch.Test.MyEnergyCommunity;

public partial class ClientTest
{
  [Fact]
  public void GetMeasurementsTest()
  {
    var device = Data.MyEnergyCommunityDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddHours(-1),
      To = DateTime.UtcNow
    };

    var measurements = Client
      .GetMeasurements(device.ToProvisionDevice(), period)
      .SelectMany(Functions.Id)
      .ToList();
    Assert.True(measurements.Count > 5);
  }

  [Fact]
  public async Task GetMeasurementsAsyncTest()
  {
    var device = Data.MyEnergyCommunityDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddHours(-1),
      To = DateTime.UtcNow
    };

    var measurements = await Client
      .GetMeasurementsAsync(device.ToProvisionDevice(), period)
      .Await()
      .Then(buckets => buckets
        .SelectMany(Functions.Id)
        .ToList());
    Assert.True(measurements.Count > 5);
  }
}
