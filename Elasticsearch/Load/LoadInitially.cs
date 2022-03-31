using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Elasticsearch {
public partial interface IClient {
  public void LoadInitially();

  public Task LoadInitiallyAsync();
}

public partial class Client : IClient {
  public void LoadInitially() {
    var task = LoadInitiallyAsync();
    task.Wait();
  }

  public async Task LoadInitiallyAsync() {
    var period = new Period {
      From = DateTime.MinValue.ToUniversalTime(),
      To = DateTime.UtcNow,
    };

    IndexLog(new Log(
        LogType.LoadBegin, Source, new Log.KnownData { Period = period }));

    var measurements = new List<Measurement>();

    foreach (var provider in Providers) {
      var searchDevicesResponse = SearchDevices(provider.Source);
      var devices = searchDevicesResponse.Sources();

      var providerMeasurements = new List<Measurement> {};

      foreach (var device in devices) {
        var deviceMeasurements =
            await provider.GetMeasurementsAsync(device, period);

        providerMeasurements.AddRange(deviceMeasurements);
      }

      measurements.AddRange(providerMeasurements);

      Logger.LogDebug($"Got {providerMeasurements.Count} measurements " +
                      $"from {provider.Source}");
    }

    await IndexMeasurementsAsync(measurements);

    IndexLog(new Log(
        LogType.LoadEnd, Source, new Log.KnownData { Period = period }));

    Logger.LogDebug($"Indexed {measurements.Count} measurements");
  }
}
}
