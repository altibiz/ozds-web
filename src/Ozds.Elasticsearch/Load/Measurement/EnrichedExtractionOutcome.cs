namespace Ozds.Elasticsearch;

public readonly record struct EnrichedExtractionOutcome
(ExtractionDevice Device,
 IEnumerable<EnrichedExtractionOutcomeItem> Items);

public readonly record struct EnrichedExtractionOutcomeAsync
(ExtractionDevice Device,
 IAsyncEnumerable<EnrichedExtractionOutcomeItem> Items);

public readonly record struct EnrichedExtractionOutcomeItem
(ExtractionPlanItem Original,
 ExtractionPlanItem? Next,
 IExtractionBucket<LoadMeasurement> Bucket);
