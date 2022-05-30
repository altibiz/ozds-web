namespace Ozds.Elasticsearch;

public readonly record struct AsyncMeasurementExtraction
(ExtractionDevice Device,
 Period Period,
 IAsyncEnumerable<MeasurementExtractionItem> Items);

public readonly record struct MeasurementExtraction
(ExtractionDevice Device,
 Period Period,
 IEnumerable<MeasurementExtractionItem> Items);

public readonly record struct MeasurementExtractionItem
(ExtractionPlanItem Original,
 ExtractionPlanItem? Next,
 IExtractionBucket<ExtractionMeasurement> Bucket);
