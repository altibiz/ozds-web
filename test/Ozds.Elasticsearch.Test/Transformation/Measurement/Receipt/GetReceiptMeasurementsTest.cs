using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  // TODO: test
  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public async Task GetReceiptMeasurementsAsyncTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    await SetupMeasurementsAsync(device, measurements);
  }

  // TODO: test
  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public void GetReceiptMeasurementsTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    SetupMeasurements(device, measurements);
  }
}
