using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task PlanSourceExtractionAsyncTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    await SetupDevicesAsync(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var extractionPlans = await Client
      .PlanSourceExtractionAsync(
        Elasticsearch.MeasurementFaker.Client.FakeSource);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void PlanSourceExtractionTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var extractionPlans = Client
      .PlanSourceExtraction(
        Elasticsearch.MeasurementFaker.Client.FakeSource);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void PlanSourceExtractionInPeriodTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);
    var period =
      new Period
      {
        From = DateTime.UtcNow.AddMinutes(-5),
        To = DateTime.UtcNow
      };

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var extractionPlans = Client
      .PlanSourceExtraction(
        Elasticsearch.MeasurementFaker.Client.FakeSource,
        period);
  }
}
