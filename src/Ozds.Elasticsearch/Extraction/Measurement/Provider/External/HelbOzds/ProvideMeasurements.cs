namespace Ozds.Elasticsearch.HelbOzds;

public partial interface IClient : IMeasurementProvider { };

public sealed partial class Client : IClient
{
  public string Source { get => Client.HelbOzdsSource; }

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAwait(
      ProvisionDevice device,
      Period? period = null) =>
    throw new NotImplementedException();

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  GetMeasurementsAsync(
      ProvisionDevice device,
      Period? period = null) =>
    throw new NotImplementedException();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      ProvisionDevice device,
      Period? period = null) =>
    throw new NotImplementedException();

  private ExtractionMeasurement
  Convert(
      Measurement measurement) =>
    throw new NotImplementedException();
}
