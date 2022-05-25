namespace Ozds.Elasticsearch;

public class FakeMeasurementLoader : IMeasurementLoader
{
  public Task LoadMeasurementsAwait(
      EnrichedExtractionOutcomeAsync _) =>
    Task.CompletedTask;

  public Task LoadMeasurementsAsync(
      EnrichedExtractionOutcomeAsync _) =>
    Task.CompletedTask;

  public void LoadMeasurements(
      EnrichedExtractionOutcome _)
  { }
}
