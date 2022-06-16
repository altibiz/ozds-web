using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public async Task GetDashboardMeasurementsAsyncTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    await SetupMeasurementsAsync(device, measurements);

    var dashboardMeasurements = await Client
      .GetDashboardMeasurementsByDeviceAsync(device.Id, period);
    Assert.Equal(measurements.Count(), dashboardMeasurements.Count());
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public void GetDashboardMeasurementsTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    SetupMeasurements(device, measurements);

    Logger.LogDebug(period.ToString());
    var dashboardMeasurements = Client
      .GetDashboardMeasurementsByDevice(device.Id, period);
    Assert.Equal(measurements.Count(), dashboardMeasurements.Count());
  }
}
