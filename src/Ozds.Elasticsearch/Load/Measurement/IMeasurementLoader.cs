namespace Ozds.Elasticsearch;

public interface IMeasurementLoader
{
  public Task LoadMeasurementsAwait(
      EnrichedExtractionOutcomeAsync measurements);

  public Task LoadMeasurementsAsync(
      EnrichedExtractionOutcomeAsync measurements);

  public void LoadMeasurements(
      EnrichedExtractionOutcome measurements);
}
