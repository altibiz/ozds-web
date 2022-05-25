using Xunit;
using Ozds.Util;

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

    // NOTE: preparation for searching
    Thread.Sleep(1000);
    foreach (var device in extractionDevices)
    {
      var now = DateTime.UtcNow;
      var period =
        new Period
        {
          From = device.ExtractionStart,
          To = now
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
      Assert.Equal(extractionOutcome.Device, device);
      Assert.Equal(
        extractionOutcomeItems.Select(item => item.Original),
        extractionPlan.Items);
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

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);
      Assert.Equal(extractionOutcome.Device, device);
      Assert.Equal(
        extractionOutcome.Items.Select(item => item.Original),
        extractionPlan.Items);
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
        From = now.AddMinutes(-5),
        To = now
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

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);
      Assert.Equal(extractionOutcome.Device, device);
      Assert.Equal(
        extractionOutcome.Items.Select(item => item.Original),
        extractionPlan.Items);
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
      Client.IndexLog(
        new Log(
          LogType.MissingData,
          device.Id,
          new Log.KnownData
          {
            Period = missingDataPeriod,
            NextExtraction = missingDataNextExtraction,
          }));

      // NOTE: preparation for searching
      Thread.Sleep(1000);
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

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);

      var extractionOutcomeItems = extractionOutcome.Items.ToList();
      Assert.Equal(
        extractionOutcomeItems.Select(item => item.Original),
        extractionPlan.Items);

      var missingDataItem = extractionOutcomeItems
        .FirstOrDefault(item => item.Original.Due == missingDataNextExtraction);
      Assert.NotEqual(default, missingDataItem);
      Assert.Equal(missingDataItem.Original.Period, missingDataPeriod);
      Assert.Equal(0, missingDataItem.Original.Retries);
      Assert.Equal(missingDataItem.Original.Timeout, device.ExtractionTimeout);
      Assert.Null(missingDataItem.Next);

      extractionOutcomeItems.Remove(missingDataItem);
      Assert.Equal(extractionOutcome.Device, device);
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