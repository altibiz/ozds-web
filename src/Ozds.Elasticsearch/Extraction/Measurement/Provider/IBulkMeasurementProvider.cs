namespace Ozds.Elasticsearch;

public interface IBulkMeasurementProvider : IMeasurementSource
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAsync(
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      Period? period = null);
}
