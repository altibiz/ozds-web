using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public static partial class Loader {
  public static async Task LoadInitialAsync(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator) {
    await elasticsearchClient.AddLoaderLogAsync(
        new Log { timestamp = DateTime.Now, type = LogType.LoadBegin });

    var measurements = new List<Measurement>();

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      var providerMeasurements =
          await measurementProvider.GetMeasurementsSortedAsync("", "");

      measurements.AddRange(providerMeasurements);
    }

    await elasticsearchClient.AddMeasurementsAsync(measurements);

    await elasticsearchClient.AddLoaderLogAsync(
        new Log { timestamp = DateTime.Now, type = LogType.LoadEnd });
  }
}
}
