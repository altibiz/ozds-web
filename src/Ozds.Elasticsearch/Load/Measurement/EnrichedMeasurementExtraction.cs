namespace Ozds.Elasticsearch;

public readonly record struct EnrichedMeasurementExtractionAsync
(ExtractionDevice Device,
 Period Period,
 IAsyncEnumerable<EnrichedMeasurementExtractionItem> Items);

public readonly record struct EnrichedMeasurementExtraction
(ExtractionDevice Device,
 Period Period,
 IEnumerable<EnrichedMeasurementExtractionItem> Items);

public readonly record struct EnrichedMeasurementExtractionItem
(ExtractionPlanItem Original,
 ExtractionPlanItem? Next,
 IExtractionBucket<LoadMeasurement> Bucket);
