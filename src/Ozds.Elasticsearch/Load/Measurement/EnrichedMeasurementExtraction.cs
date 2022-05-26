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

public static class EnrichedMeasurementExtractionExtensions
{

  public static EnrichedMeasurementExtraction Enrich(
      this MeasurementExtraction measurement,
      Func<ExtractionMeasurement, LoadMeasurement> enrich) =>
    new EnrichedMeasurementExtraction
    {
      Device = measurement.Device,
      Period = measurement.Period,
      Items = measurement.Items
        .Select(item =>
          new EnrichedMeasurementExtractionItem
          {
            Original = item.Original,
            Next = item.Next,
            Bucket =
              new ExtractionBucket<LoadMeasurement>(
                item.Bucket.Period,
                item.Bucket.Select(enrich))
          })
    };

  public static EnrichedMeasurementExtractionAsync Enrich(
      this MeasurementExtractionAsync measurement,
      Func<ExtractionMeasurement, LoadMeasurement> enrich) =>
    new EnrichedMeasurementExtractionAsync
    {
      Device = measurement.Device,
      Period = measurement.Period,
      Items = measurement.Items
        .Select(item =>
          new EnrichedMeasurementExtractionItem
          {
            Original = item.Original,
            Next = item.Next,
            Bucket =
              new ExtractionBucket<LoadMeasurement>(
                item.Bucket.Period,
                item.Bucket.Select(enrich))
          })
    };
}
