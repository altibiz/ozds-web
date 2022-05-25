using Ozds.Util;

namespace Ozds.Elasticsearch;

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
                .SelectFilter(missingDataLog =>
                  missingDataLog.Data.Period is null ||
                  missingDataLog.Data.NextExtraction is null ?
                    null as ExtractionPlanItem?
                  : new ExtractionPlanItem
                  {
                    Period = missingDataLog.Data.Period,
                    Retries = missingDataLog.Data.Retries ?? 0,
                    Timeout = device.ExtractionTimeout,
                    Due = missingDataLog.Data.NextExtraction.Value,
                    ShouldValidate =
                        missingDataLog.Data.ShouldValidate ?? false,
                    Error = missingDataLog.Data.Error
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
                    To = period?.To ?? now.Subtract(device.ExtractionOffset)
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
                      ShouldValidate =
                        device.LastValidation + device.ValidationInterval >
                        now,
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
              .SelectFilter(missingDataLog =>
                missingDataLog.Data.Period is null ||
                missingDataLog.Data.NextExtraction is null ?
                  null as ExtractionPlanItem?
                : new ExtractionPlanItem
                {
                  Period = missingDataLog.Data.Period,
                  Retries = missingDataLog.Data.Retries ?? 0,
                  Timeout = device.ExtractionTimeout,
                  Due = missingDataLog.Data.NextExtraction.Value,
                  ShouldValidate =
                      missingDataLog.Data.ShouldValidate ?? false,
                  Error = missingDataLog.Data.Error
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
                  To = period?.To ?? now.Subtract(device.ExtractionOffset)
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
                    // TODO: somehow
                    ShouldValidate = false,
                  }))
        }
    };
}
