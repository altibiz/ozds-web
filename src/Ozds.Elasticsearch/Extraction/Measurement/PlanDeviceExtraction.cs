using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public const int DefaultMeasurementsPerExtractionPlanItem = 100;

  public Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        DefaultMeasurementsPerExtractionPlanItem);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    // TODO: filter by nextExtraction < now for missing data
    (SearchLogsSortedByPeriodAsync(
        LogType.MissingData, device.Id, period: period)
      .Then(ISearchResponseExtensions.Sources),
     SearchLogsSortedByPeriodAsync(
        LogType.LoadEnd, device.Id, size: 1, period: period)
      .Then(ISearchResponseExtensions.FirstOrDefault),
     SearchLogsSortedByPeriodAsync(
        LogType.LoadEnd, device.Source, size: 1, period: period)
      .Then(ISearchResponseExtensions.FirstOrDefault),
     SearchMeasurementsByDeviceSortedAsync(
        device.Id, period: period, size: 1)
      .Then(ISearchResponseExtensions.FirstOrDefault),
     Task.FromResult(DateTime.UtcNow))
    .Await()
    .Then(responses => responses switch
      {
        (var missingDataLogs,
         var lastDeviceLoad,
         var lastSourceLoad,
         var lastMeasurement,
         var now) =>
          new ExtractionPlan
          {
            Device = device,
            Items =
              missingDataLogs
                .Select(missingDataLog =>
                  new ExtractionPlanItem
                  {
                    Period =
                      missingDataLog.Data.Period.ThrowWhenNull(),
                    Retries =
                      missingDataLog.Data.Retries.ThrowWhenNull(),
                    Timeout = device.ExtractionTimeout,
                    Due =
                      missingDataLog.Data.NextExtraction.ThrowWhenNull(),
                    ShouldValidate =
                      missingDataLog.Data.ShouldValidate ?? false,
                  })
                .Concat(
                  new Period
                  {
                    From =
                      Objects.Max(
                        device.ExtractionStart,
                        period?.From,
                        lastDeviceLoad?.Data.Period?.To,
                        lastSourceLoad?.Data.Period?.To,
                        lastMeasurement?.MeasurementTimestamp),
                    To = period?.To ?? now
                  }
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
                      ShouldValidate = false,
                    }))
          }
      });

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem) =>
    // TODO: filter by nextExtraction < now for missing data
    (SearchLogsSortedByPeriod(
        LogType.MissingData, device.Id, period: period)
      .Sources(),
     SearchLogsSortedByPeriod(
        LogType.LoadEnd, device.Id, size: 1, period: period)
      .FirstOrDefault(),
     SearchLogsSortedByPeriod(
        LogType.LoadEnd, device.Source, size: 1, period: period)
      .FirstOrDefault(),
     SearchMeasurementsByDeviceSorted(
        device.Id, period: period, size: 1)
      .FirstOrDefault(),
     DateTime.UtcNow) switch
    {
      (var missingDataLogs,
       var lastDeviceLoad,
       var lastSourceLoad,
       var lastMeasurement,
       var now) =>
        new ExtractionPlan
        {
          Device = device,
          Items =
            missingDataLogs
              .Select(missingDataLog =>
                new ExtractionPlanItem
                {
                  Period =
                    missingDataLog.Data.Period.ThrowWhenNull(),
                  Retries =
                    missingDataLog.Data.Retries.ThrowWhenNull(),
                  Timeout = device.ExtractionTimeout,
                  Due =
                    missingDataLog.Data.NextExtraction.ThrowWhenNull(),
                  ShouldValidate =
                    missingDataLog.Data.ShouldValidate ?? false,
                })
              .Concat(
                new Period
                {
                  From =
                    Objects.Max(
                      device.ExtractionStart,
                      period?.From,
                      lastDeviceLoad?.Data.Period?.To,
                      lastSourceLoad?.Data.Period?.To,
                      lastMeasurement?.MeasurementTimestamp),
                  To = period?.To ?? now
                }
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
                      // TODO: ??
                      ShouldValidate = false,
                  }))
        }
    };
}
