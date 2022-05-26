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
  public static MissingDataLog ToMissingDataLog(
      this ExtractionPlanItem item,
      ExtractionDevice device) =>
    new(
      device.Id,
      item.Period,
      item.Due,
      item.Retries,
      item.ShouldValidate,
      item.Error ?? "");
}
