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
