namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public IEnumerable<Measurement> LoadMeasurements(Period? period = null);

  public Task<IEnumerable<Measurement>> LoadMeasurementsAsync(
      Period? period = null);
};

public partial class Client : IClient
{
  public IEnumerable<Measurement> LoadMeasurements(Period? period = null)
  {
    var task = LoadMeasurementsAsync(period);
    task.Wait();
    return task.Result;
  }

  public async Task<IEnumerable<Measurement>> LoadMeasurementsAsync(
      Period? period = null)
  {
    var measurements = new List<Measurement> { };

    foreach (var provider in Providers)
    {
      measurements.AddRange(
          await LoadSourceMeasurementsAsync(provider.Source, period));
    }

    Logger.LogDebug($"Fetched {measurements.Count} measurements");

    return measurements;
  }
}
