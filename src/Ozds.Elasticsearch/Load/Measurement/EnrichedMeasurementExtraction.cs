namespace Ozds.Elasticsearch;

public readonly record struct EnrichedMeasurementExtractionAsync
(ExtractionDevice Device,
 IAsyncEnumerable<EnrichedMeasurementExtractionItem> Items);

public readonly record struct EnrichedMeasurementExtraction
(ExtractionDevice Device,
 IEnumerable<EnrichedMeasurementExtractionItem> Items);

public readonly record struct EnrichedMeasurementExtractionItem
(ExtractionPlanItem Original,
 ExtractionPlanItem? Next,
 IExtractionBucket<LoadMeasurement> Bucket);
