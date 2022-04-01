using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

namespace Elasticsearch.Test;

public static partial class Data {
  public static readonly Device MyEnergyCommunityDevice =
      new Device("MyEnergyCommunity", "M9EQCU59",
          new Device.KnownSourceDeviceData { ownerId = "test-owner" },
          DeviceState.Healthy);

  public static readonly Device FakeDevice =
      new Device(Elasticsearch.MeasurementFaker.Client.FakeSource,
          Elasticsearch.MeasurementFaker.Client.FakeDeviceId, null,
          DeviceState.Healthy);

  public static IEnumerable<object[]> GenerateDevices() {
    yield return new object[] { new Device[] { Data.FakeDevice } };
  }

  public static IEnumerable<object[]> GenerateDevicesWithPeriod() {
    var now = DateTime.UtcNow.AddMinutes(-5);
    yield return new object[] { new Device[] { Data.FakeDevice },
      new Period { From = now.Subtract(s_defaultTimeSpan), To = now } };
  }

  private static TimeSpan s_defaultTimeSpan = TimeSpan.FromMinutes(5);
}

public partial class ClientTest {
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void SetupDevices(IEnumerable<Device> devices) {
    foreach (var device in devices) {
      var deviceIndexResponse = Client.IndexDevice(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = Client.GetDevice(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task SetupDevicesAsync(IEnumerable<Device> devices) {
    foreach (var device in devices) {
      var deviceIndexResponse = await Client.IndexDeviceAsync(device);
      Assert.True(deviceIndexResponse.IsValid);

      var indexedDeviceId = deviceIndexResponse.Id;
      Assert.Equal(device.Id, indexedDeviceId);

      var deviceGetResponse = await Client.GetDeviceAsync(device.Id);
      Assert.True(deviceGetResponse.IsValid);

      var gotDevice = deviceGetResponse.Source;
      Assert.Equal(device, gotDevice);
    }
  }
}
