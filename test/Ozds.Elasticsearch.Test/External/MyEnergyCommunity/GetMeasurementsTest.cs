using Xunit;

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

    var measurements = Client.GetMeasurements(device, period).ToList();
    Assert.True(measurements.Count > 5);
  }

  [Fact]
  public async void GetMeasurementsAsyncTest()
  {
    var device = Data.MyEnergyCommunityDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddHours(-1),
      To = DateTime.UtcNow
    };

    var measurements =
        (await Client.GetMeasurementsAsync(device, period)).ToList();
    Assert.True(measurements.Count > 5);
  }
}
