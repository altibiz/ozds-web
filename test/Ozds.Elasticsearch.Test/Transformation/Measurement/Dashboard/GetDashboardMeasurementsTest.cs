using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevice), MemberType = typeof(Data))]
  public async Task GetDashboardMeasurementsAsyncTest(Device device)
  {
    await SetupDeviceAsync(device);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevice), MemberType = typeof(Data))]
  public void GetDashboardMeasurementsTest(Device device)
  {
    SetupDevice(device);
  }
}
