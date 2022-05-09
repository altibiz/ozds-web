using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeMeasurementImporter : IMeasurementImporter
{
  public Task ImportMeasurementsAsync() =>
    Task.CompletedTask;

  public void ImportMeasurements() =>
    ImportMeasurementsAsync().BlockTask();
}
