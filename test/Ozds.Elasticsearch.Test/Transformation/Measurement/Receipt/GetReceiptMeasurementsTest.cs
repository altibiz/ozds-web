using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevice), MemberType = typeof(Data))]
  public async Task GetReceiptMeasurementsAsyncTest(Device device)
  {
    await SetupDeviceAsync(device);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevice), MemberType = typeof(Data))]
  public void GetReceiptMeasurementsTest(Device device)
  {
    SetupDevice(device);
  }
}
