namespace Ozds.Elasticsearch;

public class FakeMeasurementLoader : IMeasurementLoader
{
  public Task LoadMeasurementsAsync(
      EnrichedMeasurementExtractionAsync _) =>
    Task.CompletedTask;

  public void LoadMeasurements(
      EnrichedMeasurementExtraction _)
  { }
}
