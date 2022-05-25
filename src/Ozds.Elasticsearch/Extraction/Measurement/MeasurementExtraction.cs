namespace Ozds.Elasticsearch;

public readonly record struct MeasurementExtraction
(ExtractionDevice Device,
 IEnumerable<MeasurementExtractionItem> Items);

public readonly record struct MeasurementExtractionAsync
(ExtractionDevice Device,
 IAsyncEnumerable<MeasurementExtractionItem> Items);

public readonly record struct MeasurementExtractionItem
(ExtractionPlanItem Original,
 ExtractionPlanItem? Next,
 IExtractionBucket<ExtractionMeasurement> Bucket);
