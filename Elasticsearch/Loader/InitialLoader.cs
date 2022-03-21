using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public static partial class Loader {
  public static async Task LoadInitialAsync(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator) {
    await elasticsearchClient.addLoaderLogAsync(
        new Log { timestamp = DateTime.Now, type = LogType.LoadBegin });

    var measurements = new List<Measurement>();

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      var providerMeasurements =
          await measurementProvider.getMeasurementsSortedAsync("", "");

      measurements.AddRange(providerMeasurements);
    }

    await elasticsearchClient.addMeasurementsAsync(measurements);

    await elasticsearchClient.addLoaderLogAsync(
        new Log { timestamp = DateTime.Now, type = LogType.LoadEnd });
  }
}
}
