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
 ExtractionPlanItemError? Error);

public readonly record struct ExtractionPlanItemError
(ExtractionPlanItemErrorCode Code,
 string Description);

public enum ExtractionPlanItemErrorCode
{
  Provider = 0,
  Validation = 1,
  Consistency = 2,
}

public static class ExtractionPlanItemExtensions
{
  public static MissingDataLog ToMissingDataLogFor(
      this ExtractionPlanItem item,
      ExtractionDevice device) =>
    item.Error is null ?
      throw new NullReferenceException(
        "ExtractionPlanItem being converted to a MissingDataLog " +
        "should have an error")
    : new(
        resource: device.Id,
        period: item.Period,
        nextExtraction: item.Due,
        retries: item.Retries,
        shouldValidate: item.ShouldValidate,
        error: new(
          code: item.Error.Value.Code.ToExtractionPlanItemErrorCode(),
          description: item.Error.Value.Description));

  public static string ToExtractionPlanItemErrorCode(
      this ExtractionPlanItemErrorCode extractionPlanItemErrorCode) =>
    extractionPlanItemErrorCode switch
    {
      ExtractionPlanItemErrorCode.Provider =>
        MissingDataLogErrorCode.Provider,
      ExtractionPlanItemErrorCode.Validation =>
        MissingDataLogErrorCode.Validation,
      ExtractionPlanItemErrorCode.Consistency =>
        MissingDataLogErrorCode.Consistency,
      _ => throw new ArgumentException(
          "Argument is not a valid ExtractionPlanItemErrorCode"),
    };

  public static ExtractionPlanItem ToExtractionPlanItemFor(
      this MissingDataLog log,
      ExtractionDevice device) =>
    new(
      Period: log.Period,
      Due: log.NextExtraction,
      Retries: log.Retries,
      Timeout: device.ExtractionTimeout,
      ShouldValidate: log.ShouldValidate,
      Error: new(
        Code: log.ErrorData.Code.ToExtractionPlanItemErrorCode(),
        Description: log.ErrorData.Description));

  public static ExtractionPlanItemErrorCode ToExtractionPlanItemErrorCode(
      this string missingDataLogErrorCode) =>
    missingDataLogErrorCode switch
    {
      MissingDataLogErrorCode.Provider =>
        ExtractionPlanItemErrorCode.Provider,
      MissingDataLogErrorCode.Validation =>
        ExtractionPlanItemErrorCode.Validation,
      MissingDataLogErrorCode.Consistency =>
        ExtractionPlanItemErrorCode.Consistency,
      _ => throw new ArgumentException(
          "Argument is not a valid MissingDataLogErrorCode"),
    };
}
