namespace Ozds.Elasticsearch;

public interface IBulkMeasurementProvider : IMeasurementSource
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAwait(
      Period? period = null);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  GetMeasurementsAsync(
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      Period? period = null);
}
