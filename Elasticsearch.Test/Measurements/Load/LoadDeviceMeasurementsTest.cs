using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test;

public partial class ClientTest {
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void LoadDeviceMeasurementsTest(IEnumerable<Device> devices) {
    foreach (var device in devices) {
      var measurements = Client.LoadDeviceMeasurements(device);
      Assert.NotEmpty(measurements);
      Assert.All(measurements, m => Assert.Equal(m.DeviceId, device.Id));
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task LoadDeviceMeasurementsAsyncTest(
      IEnumerable<Device> devices) {
    foreach (var device in devices) {
      var measurements = await Client.LoadDeviceMeasurementsAsync(device);
      Assert.NotEmpty(measurements);
      Assert.All(measurements, m => Assert.Equal(m.DeviceId, device.Id));
    }
  }

  [Theory]
  [MemberData(
      nameof(Data.GenerateDevicesWithPeriod), MemberType = typeof(Data))]
  public async Task LoadDeviceMeasurementsInPeriodTest(
      IEnumerable<Device> devices, Period period) {
    foreach (var device in devices) {
      var measurements =
          await Client.LoadDeviceMeasurementsAsync(device, period);
      Assert.NotEmpty(measurements);
      Assert.All(measurements, m => {
        Assert.Equal(m.DeviceId, device.Id);
        Assert.InRange(m.MeasurementTimestamp, period.From, period.To);
      });
    }
  }
}
