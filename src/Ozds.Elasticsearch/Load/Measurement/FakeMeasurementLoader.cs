namespace Ozds.Elasticsearch;

public class FakeMeasurementLoader : IMeasurementLoader
{
  public Task LoadMeasurementsAsync(
      AsyncEnrichedMeasurementExtraction _) =>
    Task.CompletedTask;

  public void LoadMeasurements(
      EnrichedMeasurementExtraction _)
  { }
}
