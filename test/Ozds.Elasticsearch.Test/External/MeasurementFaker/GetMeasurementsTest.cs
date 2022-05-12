using Xunit;

namespace Ozds.Elasticsearch.Test.MeasurementFaker;

public partial class ClientTest
{
  [Fact]
  public void GetMeasurementsTest()
  {
    var device = Data.FakeDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddMinutes(-5),
      To = DateTime.UtcNow
    };
    var measurements = Client.GetMeasurements(device, period).ToList();
    Assert.NotEmpty(measurements);
    Assert.InRange(measurements.Count, 15, 25);
    Assert.All(measurements, m =>
    {
      Assert.Equal(m.DeviceId, device.Id);
      Assert.InRange(m.MeasurementTimestamp, period.From, period.To);
    });
  }

  [Fact]
  public async Task GetMeasurementsAsyncTest()
  {
    var device = Data.FakeDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddMinutes(-5),
      To = DateTime.UtcNow
    };
    var measurements =
        (await Client.GetMeasurementsAsync(device, period)).ToList();
    Assert.NotEmpty(measurements);
    Assert.InRange(measurements.Count, 15, 25);
    Assert.All(measurements, m =>
    {
      Assert.Equal(m.DeviceId, device.Id);
      Assert.InRange(m.MeasurementTimestamp, period.From, period.To);
    });
  }
}
