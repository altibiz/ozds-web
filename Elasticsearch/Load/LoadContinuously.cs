using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public void LoadContinuously(
      IMeasurementProviderIterator measurementProviderIterator,
      Period? period = null);

  public Task LoadContinuouslyAsync(
      IMeasurementProviderIterator measurementProviderIterator,
      Period? period = null);
};

public partial class Client : IClient {
  public void LoadContinuously(
      IMeasurementProviderIterator measurementProviderIterator,
      Period? period = null) {
    var task = LoadContinuouslyAsync(measurementProviderIterator, period);
    task.Wait();
  }

  public async Task LoadContinuouslyAsync(
      IMeasurementProviderIterator measurementProviderIterator,
      Period? period = null) {
    if (period == null) {
      period = await GetNextLoadPeriodAsync();
      if (period == null) {
        await LoadInitiallyAsync(measurementProviderIterator);
        return;
      }
    }

    IndexLoaderLog(
        new Log(LogType.LoadBegin, new Log.KnownData { Period = period }));

    var measurements = new List<Measurement> {};

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      var searchDevicesResponse = SearchDevices(measurementProvider.Source);
      var devices = searchDevicesResponse.Sources();
      foreach (var device in devices) {
        var providerMeasurements =
            await measurementProvider.GetMeasurementsAsync(device, period);
        measurements.AddRange(providerMeasurements);
      }
    }

    IndexLoaderLog(
        new Log(LogType.LoadEnd, new Log.KnownData { Period = period }));

    await IndexMeasurementsAsync(measurements);
  }

  private async Task<Period?> GetNextLoadPeriodAsync() {
    var lastLoadEndLog =
        (await SearchLoaderLogsSortedByPeriodAsync(LogType.LoadEnd, 1))
            .Sources()
            .FirstOrDefault();
    if (lastLoadEndLog == null || lastLoadEndLog.Data.Period == null) {
      return null;
    }

    return new Period { From = lastLoadEndLog.Data.Period.To,
      To = DateTime.Now };
  }
}
}
