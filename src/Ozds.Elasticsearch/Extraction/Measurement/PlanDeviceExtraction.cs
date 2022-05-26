using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public async Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem)
  {
    var now = DateTime.UtcNow;
    var missingDataLogs =
      await SearchMissingDataLogsSortedByPeriodAsync(
          device.Id, due: now, period: period)
        .Then(response => response.Sources());
    var loadLog =
      await GetLoadLogAsync(LoadLog.MakeId(device.Id))
        .Then(response => response.Source);

    return
      PlanDeviceExtraction(
       device,
       period,
       measurementsPerExtractionPlanItem,
       now,
       missingDataLogs,
       loadLog);
  }

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem)
  {
    var now = DateTime.UtcNow;
    var missingDataLogs =
      SearchMissingDataLogsSortedByPeriod(
          device.Id, due: now, period: period)
        .Sources();
    var loadLog =
      GetLoadLog(LoadLog.MakeId(device.Id))
        .Source;

    return
      PlanDeviceExtraction(
       device,
       period,
       measurementsPerExtractionPlanItem,
       now,
       missingDataLogs,
       loadLog);
  }

  private ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period,
      int measurementsPerExtractionPlanItem,
      DateTime now,
      IEnumerable<MissingDataLog> missingDataLogs,
      LoadLog? loadLog) =>
    new Period
    {
      From =
        Objects.Max(
          device.ExtractionStart,
          period?.From,
          loadLog?.Period.To),
      To =
        Objects.Min(
          now.Subtract(device.ExtractionOffset),
          period?.To)
    }
    .WhenNullable(period =>
      new ExtractionPlan
      {
        Device = device,
        Period = period,
        Items =
          missingDataLogs
            .Select(missingDataLog =>
              new ExtractionPlanItem
              {
                Period = missingDataLog.Period,
                Retries = missingDataLog.Retries,
                Timeout = device.ExtractionTimeout,
                Due = missingDataLog.NextExtraction,
                ShouldValidate = missingDataLog.ShouldValidate,
                Error = missingDataLog.Error
              })
            .Concat(period
              .SplitAscending(
                device.MeasurementInterval *
                measurementsPerExtractionPlanItem)
              .Select(extractionPeriod =>
                new ExtractionPlanItem
                {
                  Period = extractionPeriod,
                  Retries = 0,
                  Timeout = device.ExtractionTimeout,
                  Due = now,
                  ShouldValidate =
                    device.LastValidation + device.ValidationInterval >
                    now,
                }))
      });
}
