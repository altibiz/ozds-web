using System;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test;

public partial class ClientTest {
  [Fact]
  public void LoadDeviceMeasurementsTest() {
    var device = Data.FakeDevice;
    var period = new Period { From = DateTime.UtcNow.AddMinutes(-5),
      To = DateTime.UtcNow };

    var measurements = Client.LoadDeviceMeasurements(device);
    Assert.NotEmpty(measurements);
    Assert.All(measurements, m => Assert.Equal(m.DeviceId, device.Id));

    measurements = Client.LoadDeviceMeasurements(device, period);
        Assert.NotEmpty(measurements);
        Assert.All(measurements, m => Assert.Equal(m.DeviceId, device.Id));
  }

  [Fact]
  public async Task LoadDeviceMeasurementsAsyncTest() {
    var device = Data.FakeDevice;
    var period = new Period { From = DateTime.UtcNow.AddMinutes(-5),
      To = DateTime.UtcNow };

    var measurements = await Client.LoadDeviceMeasurementsAsync(device);
    Assert.NotEmpty(measurements);
    Assert.All(measurements, m => Assert.Equal(m.DeviceId, device.Id));

    measurements = await Client.LoadDeviceMeasurementsAsync(device, period);
        Assert.NotEmpty(measurements);
        Assert.All(measurements, m => Assert.Equal(m.DeviceId, device.Id));
  }
}
