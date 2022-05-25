namespace Ozds.Elasticsearch;

public readonly record struct ExtractionPlan
(ExtractionDevice Device,
 IEnumerable<ExtractionPlanItem> Items);

public readonly record struct ExtractionPlanItem
(Period Period,
 DateTime Due,
 int Retries,
 TimeSpan Timeout,
 bool ShouldValidate);
