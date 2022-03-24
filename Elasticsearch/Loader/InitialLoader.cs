using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public static partial class Loader {
  public static async Task LoadInitialAsync(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator) {
    await elasticsearchClient.IndexLoaderLogAsync(
        new Log { Timestamp = DateTime.Now, Type = LogType.LoadBegin });

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

    await elasticsearchClient.IndexLoaderLogAsync(
        new Log { Timestamp = DateTime.Now, Type = LogType.LoadEnd });
  }
}
}
