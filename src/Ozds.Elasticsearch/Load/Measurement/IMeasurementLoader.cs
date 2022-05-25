namespace Ozds.Elasticsearch;

public interface IMeasurementLoader
{
  public Task LoadMeasurementsAwait(
      EnrichedMeasurementExtractionAsync measurements);

  public Task LoadMeasurementsAsync(
      EnrichedMeasurementExtractionAsync measurements);

  public void LoadMeasurements(
      EnrichedMeasurementExtraction measurements);
}
