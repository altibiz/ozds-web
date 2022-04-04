using System.Threading.Tasks;
using System;
using Xunit;

namespace Elasticsearch.Test.HelbOzds
{
  public partial class ClientTest
  {
    [Fact]
    public void GetMeasurementsTest()
    {
      var device = Data.MyEnergyCommunityDevice;
      var period = new Period
      {
        From = DateTime.UtcNow.AddMinutes(-5),
        To = DateTime.UtcNow
      };

      var measurements = this.Client.GetMeasurements(device, period);
      Assert.NotNull(measurements);
    }

    [Fact]
    public async Task GetMeasurementsAsyncTest()
    {
      var device = Data.MyEnergyCommunityDevice;
      var period = new Period
      {
        From = DateTime.UtcNow.AddMinutes(-5),
        To = DateTime.UtcNow
      };

      var measurements = await this.Client.GetMeasurementsAsync(device, period);
      Assert.NotNull(measurements);
    }
  }
}
