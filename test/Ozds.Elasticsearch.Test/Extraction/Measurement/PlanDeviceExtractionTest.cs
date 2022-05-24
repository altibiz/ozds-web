using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task PlanDeviceExtractionAsyncTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    await SetupDevicesAsync(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    foreach (var device in extractionDevices)
    {
      var measurementsPerExtractionPlanItem = 20;
      var extractionPlan = await Client
        .PlanDeviceExtractionAsync(
            device,
            null,
            measurementsPerExtractionPlanItem);
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now
        };
      Assert.Equal(extractionPlan.Device, device);
      Assert.Equal(
        extractionPlan.Items.Count(),
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)));
      Assert.All(
        extractionPlan.Items,
        item =>
        {
          Assert.InRange(item.Period.From, period.From, period.To);
          Assert.InRange(item.Period.To, period.From, period.To);
          Assert.Equal(item.Retries, 0);
          Assert.Equal(item.Timeout, device.ExtractionTimeout);
        });
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void PlanDeviceExtractionTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    foreach (var device in extractionDevices)
    {
      var measurementsPerExtractionPlanItem = 20;
      var extractionPlan = Client
        .PlanDeviceExtraction(
            device,
            null,
            measurementsPerExtractionPlanItem);
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now
        };
      Assert.Equal(extractionPlan.Device, device);
      Assert.Equal(
        extractionPlan.Items.Count(),
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)));
      Assert.All(
        extractionPlan.Items,
        item =>
        {
          Assert.InRange(item.Period.From, period.From, period.To);
          Assert.InRange(item.Period.To, period.From, period.To);
          Assert.Equal(item.Retries, 0);
          Assert.Equal(item.Timeout, device.ExtractionTimeout);
        });
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void PlanDeviceExtractionInPeriodTest(
      IEnumerable<Device> devices)
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
    foreach (var device in extractionDevices)
    {
      var measurementsPerExtractionPlanItem = 20;
      var extractionPlan = Client
        .PlanDeviceExtraction(
            device,
            period,
            measurementsPerExtractionPlanItem);
      var now = DateTime.UtcNow;
      Assert.Equal(extractionPlan.Device, device);
      Assert.Equal(
        extractionPlan.Items.Count(),
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)));
      Assert.All(
        extractionPlan.Items,
        item =>
        {
          Assert.InRange(item.Period.From, period.From, period.To);
          Assert.InRange(item.Period.To, period.From, period.To);
          Assert.Equal(item.Retries, 0);
          Assert.Equal(item.Timeout, device.ExtractionTimeout);
        });
    }
  }
}
