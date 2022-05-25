namespace Ozds.Elasticsearch;

public readonly record struct ExtractionPlan
(ExtractionDevice Device,
 Period Period,
 IEnumerable<ExtractionPlanItem> Items);

public readonly record struct ExtractionPlanItem
(Period Period,
 DateTime Due,
 int Retries,
 TimeSpan Timeout,
 bool ShouldValidate,
 string? Error);

public static class ExtractionPlanItemExtensions
{
  public static Log ToMissingDataLog(
      this ExtractionPlanItem item,
      ExtractionDevice device) =>
    new(
      LogType.MissingData,
      device.Id,
      new()
      {
        Period = item.Period,
        Retries = item.Retries,
        NextExtraction = item.Due,
        ShouldValidate = item.ShouldValidate,
        Error = item.Error
      });

  public static Log ToInvalidDataLog(
      this ExtractionPlanItem item,
      ExtractionDevice device) =>
    new(
      LogType.InvalidData,
      device.Id,
      new()
      {
        Period = item.Period,
        Retries = item.Retries,
        NextExtraction = item.Due,
        ShouldValidate = item.ShouldValidate,
        Error = item.Error
      });

  public static Log ToDuplicateDataLog(
      this ExtractionPlanItem item,
      ExtractionDevice device) =>
    new(
      LogType.DuplicatedData,
      device.Id,
      new()
      {
        Period = item.Period,
        Retries = item.Retries,
        NextExtraction = item.Due,
        ShouldValidate = item.ShouldValidate,
        Error = item.Error
      });
}
