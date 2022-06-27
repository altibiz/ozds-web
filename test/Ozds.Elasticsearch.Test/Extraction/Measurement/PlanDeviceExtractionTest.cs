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
          To = now.Subtract(device.ExtractionOffset)
        };
      Assert.Equal(extractionPlan.Device, device);
      Assert.Equal(
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)),
        extractionPlan.Items.Count());
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
  public void PlanDeviceExtractionTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);

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
          To = now.Subtract(device.ExtractionOffset)
        };
      Assert.Equal(extractionPlan.Device, device);
      Assert.Equal(
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)),
        extractionPlan.Items.Count());
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
        From = DateTime.UtcNow.AddMinutes(-10),
        To = DateTime.UtcNow.AddMinutes(-5)
      };

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
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)),
        extractionPlan.Items.Count());
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
  public void PlanDeviceExtractionWithMissingDataTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);

    foreach (var device in extractionDevices)
    {
      var missingDataNow = DateTime.UtcNow;
      var missingDataNextExtraction = missingDataNow;
      var missingDataPeriod =
        new Period
        {
          From = missingDataNow.AddMinutes(-25),
          To = missingDataNow.AddMinutes(-20)
        };
      var indexMissingDataResponse = Client.IndexMissingDataLog(
        new(
          resource: device.Id,
          period: missingDataPeriod,
          nextExtraction: missingDataNextExtraction,
          retries: 5,
          shouldValidate: false,
          error: new(
            code: MissingDataLogErrorCode.Provider,
            description: "Error fetching")));

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
          To = now.Subtract(device.ExtractionOffset)
        };

      var extractionPlanItems = extractionPlan.Items.ToList();
      var missingDataItem = extractionPlanItems
        .FirstOrDefault(item => item.Due == missingDataNextExtraction);
      Assert.NotEqual(default, missingDataItem);
      Assert.Equal(missingDataPeriod, missingDataItem.Period);
      Assert.Equal(5, missingDataItem.Retries);
      Assert.Equal(device.ExtractionTimeout, missingDataItem.Timeout);

      extractionPlanItems.Remove(missingDataItem);
      Assert.Equal(device, extractionPlan.Device);
      Assert.Equal(
        Math.Ceiling(
          period.Span.TotalSeconds /
          (device.MeasurementInterval.TotalSeconds *
          measurementsPerExtractionPlanItem)),
        extractionPlanItems.Count());
      Assert.All(
        extractionPlanItems,
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
