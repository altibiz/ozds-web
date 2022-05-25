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
     Task.FromResult(DateTime.UtcNow))
    .Await()
    .Then(responses => responses switch
      {
        (var missingDataLogs,
         var lastDeviceLoad,
         var now) =>
          new Period
          {
            From =
              Objects.Max(
                device.ExtractionStart,
                period?.From,
                lastDeviceLoad?.Data.Period?.To),
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
            })
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
     DateTime.UtcNow) switch
    {
      (var missingDataLogs,
       var lastDeviceLoad,
       var now) =>
        new Period
        {
          From =
            Objects.Max(
              device.ExtractionStart,
              period?.From,
              lastDeviceLoad?.Data.Period?.To),
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
                      // TODO: somehow
                      ShouldValidate = false,
                    }))
          })
    };
}
