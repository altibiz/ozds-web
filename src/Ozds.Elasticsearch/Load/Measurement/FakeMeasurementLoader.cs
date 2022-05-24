namespace Ozds.Elasticsearch;

public class FakeMeasurementLoader : IMeasurementLoader
{
  public Task LoadMeasurementsAwait(
      IAsyncEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    Task.CompletedTask;

  public Task LoadMeasurementsAsync(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    Task.CompletedTask;

  public void LoadMeasurements(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements)
  { }
}
