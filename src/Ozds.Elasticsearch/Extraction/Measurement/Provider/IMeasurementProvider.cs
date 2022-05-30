namespace Ozds.Elasticsearch;

public interface IMeasurementProvider : IMeasurementSource
{
  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAsync(
      ProvisionDevice device,
      Period? period = null);

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      ProvisionDevice device,
      Period? period = null);
}
