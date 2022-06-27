using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public async Task<ExtractionPlan>
  PlanDeviceExtractionAsync(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds)
  {
    var now = DateTime.UtcNow;
    var missingDataLogs =
      await SearchExtractionMissingDataLogsAsync(
          device.Id,
          due: now,
          maxRetries: device.ExtractionRetries,
          period: period,
          size: missingDataExtractionPlanItemsLimit)
        .Then(response => response.Sources());
    var loadLog =
      await GetLoadLogAsync(LoadLog.MakeId(device.Id))
        .Then(response => response.Source);

    return
      PlanDeviceExtraction(
       device,
       period,
       measurementsPerExtractionPlanItem,
       missingDataExtractionPlanItemsLimit,
       loadExtractionSpanLimitInSeconds,
       now,
       missingDataLogs,
       loadLog);
  }

  public ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period = null,
      int measurementsPerExtractionPlanItem =
        IMeasurementExtractor.DefaultMeasurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit =
        IMeasurementExtractor.DefaultMissingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds =
        IMeasurementExtractor.DefaultLoadExtractionSpanLimitInSeconds)
  {
    var now = DateTime.UtcNow;
    var missingDataLogs =
      SearchExtractionMissingDataLogs(
          device.Id,
          due: now,
          maxRetries: device.ExtractionRetries,
          period: period,
          size: missingDataExtractionPlanItemsLimit)
        .Sources();
    var loadLog =
      GetLoadLog(LoadLog.MakeId(device.Id))
        .Source;

    return
      PlanDeviceExtraction(
       device,
       period,
       measurementsPerExtractionPlanItem,
       missingDataExtractionPlanItemsLimit,
       loadExtractionSpanLimitInSeconds,
       now,
       missingDataLogs,
       loadLog);
  }

  private ExtractionPlan
  PlanDeviceExtraction(
      ExtractionDevice device,
      Period? period,
      int measurementsPerExtractionPlanItem,
      int missingDataExtractionPlanItemsLimit,
      int loadExtractionSpanLimitInSeconds,
      DateTime now,
      IEnumerable<MissingDataLog> missingDataLogs,
      LoadLog? loadLog)
  {
    var extractionOffset = now.Subtract(device.ExtractionOffset);
    var from =
      Objects.Min(
        extractionOffset,
        Objects.Max(
          device.ExtractionStart,
          period?.From,
          loadLog?.Period.To));
    var to =
      Objects.Max(
        from,
        Objects.Min(
          extractionOffset,
          period?.To));
    var optimizedPeriod =
      new Period
      {
        From = from,
        To = to,
      }.LimitTo(TimeSpan
        .FromSeconds(loadExtractionSpanLimitInSeconds));

    var shouldValidate =
      loadLog?.LastValidation is null ? true
      : now >= loadLog.LastValidation + device.ValidationInterval;

    return
      new ExtractionPlan
      {
        Device = device,
        Period = optimizedPeriod,
        Items =
          missingDataLogs
            .Select(missingDataLog => missingDataLog
              .ToExtractionPlanItemFor(device))
            .Concat(optimizedPeriod
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
                  ShouldValidate = shouldValidate,
                }))
      };
  }
}
