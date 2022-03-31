using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Elasticsearch {
public partial interface IClient {
  public void LoadContinuously(Period? period = null);

  public Task LoadContinuouslyAsync(Period? period = null);
};

public partial class Client : IClient {
  public void LoadContinuously(Period? period = null) {
    var task = LoadContinuouslyAsync(period);
    task.Wait();
  }

  public async Task LoadContinuouslyAsync(Period? period = null) {
    if (period == null) {
      period = await GetNextLoadPeriodAsync();
      if (period == null) {
        await LoadInitiallyAsync();
        return;
      }
    }

    IndexLog(new Log(LogType.LoadBegin, new Log.KnownData { Period = period }));

    var measurements = new List<Measurement> {};

    foreach (var measurementProvider in Providers) {
      var searchDevicesResponse = SearchDevices(measurementProvider.Source);
      var devices = searchDevicesResponse.Sources();

      var providerMeasurements = new List<Measurement> {};

      foreach (var device in devices) {
        var deviceMeasurements =
            (await measurementProvider.GetMeasurementsAsync(device, period))
                .ToList();

        providerMeasurements.AddRange(deviceMeasurements);

        Logger.LogDebug($"Got {deviceMeasurements.Count} measurements " +
                        $"from {device.Id}");
      }

      measurements.AddRange(providerMeasurements);

      Logger.LogDebug($"Got {providerMeasurements.Count} measurements " +
                      $"from {measurementProvider.Source}");
    }

    IndexLog(new Log(LogType.LoadEnd, new Log.KnownData { Period = period }));

    await IndexMeasurementsAsync(measurements);

    Logger.LogDebug($"Indexed {measurements.Count} measurements");
  }

  private async Task<Period?> GetNextLoadPeriodAsync() {
    var lastLoadEndLog =
        (await SearchLogsSortedByPeriodAsync(LogType.LoadEnd, 1))
            .Sources()
            .FirstOrDefault();
    if (lastLoadEndLog == null || lastLoadEndLog.Data.Period == null) {
      return null;
    }

    return new Period { From = lastLoadEndLog.Data.Period.To,
      To = DateTime.UtcNow };
  }
}
}
