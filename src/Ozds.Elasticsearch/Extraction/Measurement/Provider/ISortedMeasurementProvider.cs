namespace Ozds.Elasticsearch;

public interface ISortedMeasurementProvider : IMeasurementSource
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsSortedAsync(
      ProvisionDevice device,
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsSorted(
      ProvisionDevice device,
      Period? period = null);
}
