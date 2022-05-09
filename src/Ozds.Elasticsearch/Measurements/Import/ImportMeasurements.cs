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
      .ThenTask(measurements =>
        IndexMeasurementsAsync(measurements));

  public void ImportMeasurements() =>
    ImportMeasurementsAsync().BlockTask();
}
