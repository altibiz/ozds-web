namespace Ozds.Elasticsearch;

public class FakeMeasurementLoader : IMeasurementLoader
{
  public Task LoadMeasurementsAwait(
      EnrichedMeasurementExtractionAsync _) =>
    Task.CompletedTask;

  public Task LoadMeasurementsAsync(
      EnrichedMeasurementExtractionAsync _) =>
    Task.CompletedTask;

  public void LoadMeasurements(
      EnrichedMeasurementExtraction _)
  { }
}
