using Xunit;

namespace Ozds.Elasticsearch.Test;

public static partial class Data
{
  public static readonly Device MyEnergyCommunityDevice =
    new Device(
      "MyEnergyCommunity",
      "W1N2CSTX",
      new Device.SourceDeviceDataType
      {
        OwnerId = "test-owner"
      },
      new Device.OwnerDataType(
        "HelbOzds",
        "TestCenterId",
        "TestCenterUserId",
        "TestOwnerId",
        "TestOwnerUserId"),
      new Device.MeasurementDataType(
        ((int)TimeSpan.FromSeconds(10).TotalSeconds),
         DateTime.UtcNow.AddMinutes(-10),
        ((int)TimeSpan.FromSeconds(30).TotalSeconds),
        ((int)TimeSpan.FromMinutes(1).TotalSeconds),
        5,
        ((int)TimeSpan.FromHours(1).TotalSeconds)),
      new Device.StateDataType(
        DeviceState.Active));

  public static readonly Device FakeDevice =
    new Device(
      Elasticsearch.MeasurementFaker.Client.FakeSource,
      Elasticsearch.MeasurementFaker.Client.FakeDeviceId,
      null,
      new Device.OwnerDataType(
        "HelbOzds",
        "TestCenterId",
        "TestCenterUserId",
        "TestOwnerId",
        "TestOwnerUserId"),
      new Device.MeasurementDataType(
        Elasticsearch.MeasurementFaker.Client.MeasurementIntervalInSeconds,
         DateTime.UtcNow.AddMinutes(-10),
        ((int)TimeSpan.FromSeconds(30).TotalSeconds),
        ((int)TimeSpan.FromMinutes(1).TotalSeconds),
        5,
        ((int)TimeSpan.FromHours(1).TotalSeconds)),
      new Device.StateDataType(
        DeviceState.Active));

  public static IEnumerable<object[]> GenerateDevices()
  {
    yield return
      new object[]
      {
        new Device[]
        {
          Data.FakeDevice
        }
      };
  }

  public static IEnumerable<object[]> GenerateDevicesWithPeriod()
  {
    var now = DateTime.UtcNow.AddMinutes(-5);
    yield return
      new object[]
      {
        new Device[]
        {
          Data.FakeDevice
        },
        new Period
        {
          From = now.Subtract(s_defaultTimeSpan),
          To = now
        }
      };
  }

  public static IEnumerable<object[]> GenerateDevice()
  {
    yield return
      new object[]
      {
        Data.FakeDevice
      };
  }

  private static TimeSpan s_defaultTimeSpan = TimeSpan.FromMinutes(5);
}

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task SetupDevicesAsync(IEnumerable<Device> devices)
  {
    foreach (var device in devices)
    {
      await SetupDeviceAsync(device);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void SetupDevices(IEnumerable<Device> devices)
  {
    foreach (var device in devices)
    {
      SetupDevice(device);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevice), MemberType = typeof(Data))]
  public async Task SetupDeviceAsync(Device device)
  {
    var deviceIndexResponse = await Client.IndexDeviceAsync(device);
    Assert.True(deviceIndexResponse.IsValid);

    var indexedDeviceId = deviceIndexResponse.Id;
    Assert.Equal(device.Id, indexedDeviceId);

    var deviceGetResponse = await Client.GetDeviceAsync(device.Id);
    Assert.True(deviceGetResponse.IsValid);

    var gotDevice = deviceGetResponse.Source;
    Assert.Equal(device, gotDevice);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevice), MemberType = typeof(Data))]
  public void SetupDevice(Device device)
  {
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
