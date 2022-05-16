using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<IEnumerable<Measurement>> LoadMeasurementsAsync(
      Period? period = null);

  public IEnumerable<Measurement> LoadMeasurements(
      Period? period = null);
};

public partial class Client : IClient
{
  public Task<IEnumerable<Measurement>> LoadMeasurementsAsync(
      Period? period = null) =>
    Providers
      .Select(provider =>
        LoadSourceMeasurementsAsync(provider.Source, period))
      .Await()
      .Then(Enumerables.Flatten)
      .Then(Enumerable.ToList)
      .ThenWith(measurements =>
        Logger.LogDebug(
          $"Fetched {measurements.Count} measurements for {period}"))
      .Then(Enumerable.AsEnumerable);

  public IEnumerable<Measurement> LoadMeasurements(
      Period? period = null) =>
    Providers
      .SelectMany(provider =>
        LoadSourceMeasurements(provider.Source, period))
      .ToList()
      .WithNullable(measurements =>
         Logger.LogDebug(
           $"Fetched {measurements.Count} measurements for {period}"));
}
