namespace Ozds.Elasticsearch;

public readonly record struct ExtractionOutcome
(ExtractionDevice Device,
 IEnumerable<ExtractionOutcomeItem> Items);

public readonly record struct ExtractionOutcomeAsync
(ExtractionDevice Device,
 IAsyncEnumerable<ExtractionOutcomeItem> Items);

public readonly record struct ExtractionOutcomeItem
(ExtractionPlanItem Original,
 ExtractionPlanItem? Next,
 IExtractionBucket<ExtractionMeasurement> Bucket);

public readonly record struct ExtractionPlan
(ExtractionDevice Device,
 IEnumerable<ExtractionPlanItem> Items);

public readonly record struct ExtractionPlanItem
(Period Period,
 DateTime Due,
 int Retries,
 TimeSpan Timeout,
 bool ShouldValidate);
