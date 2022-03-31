using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Elasticsearch;

public partial interface IClient {
  public IEnumerable<Measurement> LoadSourceMeasurements(
      string source, Period? period = null);

  public Task<IEnumerable<Measurement>> LoadSourceMeasurementsAsync(
      string source, Period? period = null);
}

public partial class Client : IClient {
  public IEnumerable<Measurement> LoadSourceMeasurements(
      string source, Period? period = null) {
    var task = LoadSourceMeasurementsAsync(source, period);
    task.Wait();
    return task.Result;
  }

  public async Task<IEnumerable<Measurement>> LoadSourceMeasurementsAsync(
      string source, Period? period = null) {
    if (period == null) {
      period = await FetchLoadPeriodAsync(source);
      if (period == null) {
        return new List<Measurement> {};
      }
    }

    var provider = Providers.Find(p => p.Source == source);
    if (provider is null) { return new List<Measurement> {}; }

    IndexLog(new Log(LogType.LoadBegin,
        new Log.KnownData { Period = period, Source = provider.Source }));

        var searchDevicesResponse = SearchDevices(provider.Source);
        var devices = searchDevicesResponse.Sources();

        var measurements = new List<Measurement> {};

        foreach (var device in devices) {
          measurements.AddRange(
              await LoadDeviceMeasurementsAsync(device, period));
        }

        Logger.LogDebug($"Got {measurements.Count} measurements " +
                        $"from {provider.Source}");

        IndexLog(new Log(LogType.LoadEnd,
            new Log.KnownData { Period = period, Source = provider.Source }));

        return measurements;
  }

  private async Task<Period?> FetchLoadPeriodAsync(string source) {
    var lastLoadEndLog = (await SearchLoadLogsSortedByPeriodAsync(Source, 1))
                             .Sources()
                             .FirstOrDefault();

    if (lastLoadEndLog?.Data?.Period?.To is not null) {
      return new Period { From = lastLoadEndLog.Data.Period.To,
        To = DateTime.UtcNow };
    }

    return null;
  }
}
