using Xunit;
using Ozds.Extensions;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task ExecuteDeviceExtractionPlanAsyncTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    await SetupDevicesAsync(devices);

    foreach (var device in extractionDevices)
    {
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now.Subtract(device.ExtractionOffset)
        };
      var measurementsPerExtractionPlanItem = 20;
      var extractionPlan = await Client
        .PlanDeviceExtractionAsync(
            device,
            null,
            measurementsPerExtractionPlanItem);

      var extractionOutcome = Client
        .ExecuteExtractionPlanAsync(extractionPlan);
      var extractionOutcomeItems = await extractionOutcome.Items.Await();
      Assert.Equal(device, extractionOutcome.Device);
      Assert.Equal(
        extractionPlan.Items,
        extractionOutcomeItems.Select(item => item.Original));
      Assert.All(
        extractionOutcomeItems,
        item =>
        {
          Assert.Null(item.Next);
          Assert.All(
            item.Bucket,
            measurement =>
            {
              Assert.True(measurement.Validate());
            });
        });
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void ExecuteDeviceExtractionPlanTest(
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

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);
      Assert.Equal(extractionOutcome.Device, device);
      Assert.Equal(
        extractionPlan.Items,
        extractionOutcome.Items.Select(item => item.Original));
      Assert.All(
        extractionOutcome.Items,
        item =>
        {
          Assert.Null(item.Next);
          Assert.All(
            item.Bucket,
            measurement =>
            {
              Assert.True(measurement.Validate());
            });
        });
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void ExecuteDeviceExtractionPlanInPeriodTest(
      IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id);
    var extractionDevices = devices
      .Select(ExtractionDeviceExtensions.ToExtractionDevice);
    SetupDevices(devices);
    var now = DateTime.UtcNow;
    var period =
      new Period
      {
        From = now.AddMinutes(-10),
        To = now.AddMinutes(-5)
      };

    foreach (var device in extractionDevices)
    {
      var measurementsPerExtractionPlanItem = 20;
      var extractionPlan = Client
        .PlanDeviceExtraction(
            device,
            period,
            measurementsPerExtractionPlanItem);

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);
      Assert.Equal(extractionOutcome.Device, device);
      Assert.Equal(
        extractionPlan.Items,
        extractionOutcome.Items.Select(item => item.Original));
      Assert.All(
        extractionOutcome.Items,
        item =>
        {
          Assert.Null(item.Next);
          Assert.All(
            item.Bucket,
            measurement =>
            {
              Assert.True(measurement.Validate());
            });
        });
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void ExecuteDeviceExtractionPlanWithMissingDataTest(
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
      Client.IndexMissingDataLog(
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
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now.Subtract(device.ExtractionOffset)
        };
      var extractionPlan = Client
        .PlanDeviceExtraction(
            device,
            null,
            measurementsPerExtractionPlanItem);

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);

      var extractionOutcomeItems = extractionOutcome.Items.ToList();
      Assert.Equal(
        extractionOutcomeItems.Select(item => item.Original),
        extractionPlan.Items);

      var missingDataItem = extractionOutcomeItems
        .FirstOrDefault(item => item.Original.Due == missingDataNextExtraction);
      Assert.NotEqual(default, missingDataItem);
      Assert.Equal(missingDataPeriod, missingDataItem.Original.Period);
      Assert.Equal(5, missingDataItem.Original.Retries);
      Assert.Equal(device.ExtractionTimeout, missingDataItem.Original.Timeout);
      Assert.Null(missingDataItem.Next);

      extractionOutcomeItems.Remove(missingDataItem);
      Assert.Equal(device, extractionOutcome.Device);
      Assert.All(
        extractionOutcomeItems,
        item =>
        {
          Assert.Null(item.Next);
          Assert.All(
            item.Bucket,
            measurement =>
            {
              Assert.True(measurement.Validate());
            });
        });
    }
  }
}
