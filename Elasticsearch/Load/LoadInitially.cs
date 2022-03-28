using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public void LoadInitially(
      IMeasurementProviderIterator measurementProviderIterator);

  public Task LoadInitiallyAsync(
      IMeasurementProviderIterator measurementProviderIterator);
}

public partial class Client : IClient {
  public void LoadInitially(
      IMeasurementProviderIterator measurementProviderIterator) {
    var task = LoadInitiallyAsync(measurementProviderIterator);
    task.Wait();
  }

  public async Task LoadInitiallyAsync(
      IMeasurementProviderIterator measurementProviderIterator) {
    IndexLoaderLog(new Log(LogType.LoadBegin, Source));

    var measurements = new List<Measurement>();

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      foreach (var device in SearchDevices(measurementProvider.Source)
                   .Sources()) {
        var providerMeasurements =
            await measurementProvider.GetMeasurementsAsync(device);
        measurements.AddRange(providerMeasurements);
      }
    }

    await IndexMeasurementsAsync(measurements);

    IndexLoaderLog(new Log(LogType.LoadEnd, Source));
  }
}
}
