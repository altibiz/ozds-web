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
    var measurementsPerExtractionPlanItem = 20;
    var extractionPlans = await Client
      .PlanSourceExtractionAsync(
        Elasticsearch.MeasurementFaker.Client.FakeSource,
        null,
        measurementsPerExtractionPlanItem);
    foreach (var extractionPlan in extractionPlans)
    {
      var device = extractionDevices
        .FirstOrDefault(device => device == extractionPlan.Device);
      Assert.NotEqual(default, device);
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
          Assert.Equal(0, item.Retries);
          Assert.Equal(device.ExtractionTimeout, item.Timeout);
        });
    }
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
    var measurementsPerExtractionPlanItem = 20;
    var extractionPlans = Client
      .PlanSourceExtraction(
        Elasticsearch.MeasurementFaker.Client.FakeSource,
        null,
        measurementsPerExtractionPlanItem);

    foreach (var extractionPlan in extractionPlans)
    {
      var device = extractionDevices
        .FirstOrDefault(device => device == extractionPlan.Device);
      Assert.NotEqual(default, device);
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
          Assert.Equal(0, item.Retries);
          Assert.Equal(device.ExtractionTimeout, item.Timeout);
        });
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void PlanSourceExtractionInPeriodTest(IEnumerable<Device> devices)
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
      .PlanSourceExtraction(
        Elasticsearch.MeasurementFaker.Client.FakeSource,
        period,
        measurementsPerExtractionPlanItem);

    foreach (var extractionPlan in extractionPlans)
    {
      var device = extractionDevices
        .FirstOrDefault(device => device == extractionPlan.Device);
      Assert.NotEqual(default, device);
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
          Assert.Equal(0, item.Retries);
          Assert.Equal(device.ExtractionTimeout, item.Timeout);
        });
    }
  }
}
