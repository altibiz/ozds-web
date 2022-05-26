using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task LoadMeasurementsAsyncTest(
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
      var enrichedExtraction = extractionOutcome
        .Enrich(MeasurementExtensions.ToLoadMeasurement);
      await Client.LoadMeasurementsAsync(enrichedExtraction);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void LoadMeasurementsTest(
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
          To = now.Subtract(device.ExtractionOffset)
        };

      var extractionOutcome = Client
        .ExecuteExtractionPlan(extractionPlan);
      var enrichedExtraction = extractionOutcome
        .Enrich(MeasurementExtensions.ToLoadMeasurement);
      Client.LoadMeasurements(enrichedExtraction);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void LoadMeasurementsInPeriodTest(
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
      var enrichedExtraction = extractionOutcome
        .Enrich(MeasurementExtensions.ToLoadMeasurement);
      Client.LoadMeasurements(enrichedExtraction);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void LoadMeasurementsWithMissingDataTest(
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
        new MissingDataLog(
          device.Id,
          missingDataPeriod,
          missingDataNextExtraction,
          5,
          false,
          "Unknown error"));

      // NOTE: preparation for searching
      Thread.Sleep(1000);
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
      var enrichedExtraction = extractionOutcome
        .Enrich(MeasurementExtensions.ToLoadMeasurement);
      Client.LoadMeasurements(enrichedExtraction);
    }
  }
}
