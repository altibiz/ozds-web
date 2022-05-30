namespace Ozds.Elasticsearch;

public interface IBulkSortedMeasurementProvider : IMeasurementSource
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsSortedAsync(
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsSorted(
      Period? period = null);
}
