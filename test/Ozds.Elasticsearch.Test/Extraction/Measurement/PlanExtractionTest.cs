using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task PlanExtractionAsyncTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    await SetupDevicesAsync(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var measurementsPerExtractionPlanItem = 20;
    var extractionPlans = await Client
      .PlanExtractionAsync(
        null,
        measurementsPerExtractionPlanItem);
    foreach (var extractionPlan in extractionPlans)
    {
      var device = extractionDevices
        .FirstOrDefault(device => device == extractionPlan.Device);
      Assert.NotNull(device);
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now
        };
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
  public void PlanExtractionTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var measurementsPerExtractionPlanItem = 20;
    var extractionPlans = Client
      .PlanExtraction(
        null,
        measurementsPerExtractionPlanItem);

    foreach (var extractionPlan in extractionPlans)
    {
      var device = extractionDevices
        .FirstOrDefault(device => device == extractionPlan.Device);
      Assert.NotNull(device);
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now
        };
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
  public void PlanExtractionInPeriodTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    var now = DateTime.UtcNow;
    var period =
      new Period
      {
        From = now.AddMinutes(-5),
        To = now
      };
    var measurementsPerExtractionPlanItem = 20;
    var extractionPlans = Client
      .PlanExtraction(
        period,
        measurementsPerExtractionPlanItem);

    foreach (var extractionPlan in extractionPlans)
    {
      var device = extractionDevices
        .FirstOrDefault(device => device == extractionPlan.Device);
      Assert.NotNull(device);
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
