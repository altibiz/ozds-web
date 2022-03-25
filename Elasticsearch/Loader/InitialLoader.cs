using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public static partial class Loader {
  public static async Task LoadInitialAsync(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator) {
    elasticsearchClient.IndexLoaderLog(new Log(LogType.LoadBegin, Source));

    var measurements = new List<Measurement>();

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      foreach (var device in elasticsearchClient
                   .SearchDevices(measurementProvider.Source)
                   .Sources()) {
        var providerMeasurements =
            await measurementProvider.GetMeasurementsAsync(device);
        measurements.AddRange(providerMeasurements);
      }
    }

    await elasticsearchClient.IndexMeasurementsAsync(measurements);

    elasticsearchClient.IndexLoaderLog(new Log(LogType.LoadEnd, Source));
  }
}
}
