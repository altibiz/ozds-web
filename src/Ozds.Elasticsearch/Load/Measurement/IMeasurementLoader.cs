namespace Ozds.Elasticsearch;

public interface IMeasurementLoader
{
  public Task LoadMeasurementsAsync(
      AsyncEnrichedMeasurementExtraction measurements);

  public void LoadMeasurements(
      EnrichedMeasurementExtraction measurements);
}
