using System;
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
    var period = new Period {
      From = DateTime.MinValue.ToUniversalTime(),
      To = DateTime.UtcNow,
    };

    IndexLog(new Log(
        LogType.LoadBegin, Source, new Log.KnownData { Period = period }));

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

    IndexLog(new Log(
        LogType.LoadEnd, Source, new Log.KnownData { Period = period }));
  }
}
}
