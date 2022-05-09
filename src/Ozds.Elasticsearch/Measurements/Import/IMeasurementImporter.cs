namespace Ozds.Elasticsearch;

public interface IMeasurementImporter
{
  public Task ImportMeasurementsAsync();

  public void ImportMeasurements();
}
