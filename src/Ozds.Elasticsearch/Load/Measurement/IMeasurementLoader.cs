namespace Ozds.Elasticsearch;

public interface IMeasurementLoader
{
  public Task LoadMeasurementsAwait(
      IAsyncEnumerable<IExtractionBucket<LoadMeasurement>> measurements);

  public Task LoadMeasurementsAsync(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements);

  public void LoadMeasurements(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements);
}
