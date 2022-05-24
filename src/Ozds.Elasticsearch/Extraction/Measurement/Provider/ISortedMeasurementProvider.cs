namespace Ozds.Elasticsearch;

public interface ISortedMeasurementProvider : IMeasurementSource
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsSortedAwait(
      ProvisionDevice device,
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsSorted(
      ProvisionDevice device,
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  GetMeasurementsSortedAsync(
      ProvisionDevice device,
      Period? period = null);
}
