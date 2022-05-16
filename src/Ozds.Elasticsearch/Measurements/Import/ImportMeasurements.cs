using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementImporter
{
}

public partial class Client : IClient
{
  public Task ImportMeasurementsAsync() =>
    this
      .LoadMeasurementsAsync()
      .Then(Enumerable.ToList)
      .ThenWith(measurements =>
        Logger.LogInformation(
          $"Importing {measurements.Count} measurements"))
      .ThenTask(measurements =>
        IndexMeasurementsAsync(measurements));

  public void ImportMeasurements() =>
    ImportMeasurementsAsync().BlockTask();
}
