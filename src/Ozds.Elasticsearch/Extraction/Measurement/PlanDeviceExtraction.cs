using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null);

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null) =>
    // TODO: filter by nextExtraction < now for missing data
    (SearchLogsAsync(LogType.MissingData, device.Id)
       .Then(ISearchResponseExtensions.Sources),
     SearchLogsSortedByPeriodAsync(LogType.LoadEnd, device.Id, 1)
       .Then(ISearchResponseExtensions.FirstOrDefault),
     SearchLogsSortedByPeriodAsync(LogType.LoadEnd, device.Source, 1)
       .Then(ISearchResponseExtensions.FirstOrDefault),
     SearchMeasurementsByDeviceSortedAsync(device.Id, null, 1)
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
                        lastDeviceLoad?.Data.Period?.To,
                        lastSourceLoad?.Data.Period?.To,
                        lastMeasurement?.MeasurementTimestamp),
                    To = now
                  }
                  .SplitAscending(device.MeasurementInterval)
                  .Select(extractionPeriod =>
                    new ExtractionPlanItem
                    {
                      Period = extractionPeriod,
                      Retries = 0,
                      Timeout = device.ExtractionTimeout,
                      Due = now,
                      ShouldValidate = false,
                    }))
                .ToArray()
          }
      });

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null) =>
    throw new NotImplementedException();
}
