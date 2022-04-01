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
    }

    var provider = Providers.Find(p => p.Source == source);
    if (provider is null) {
      Logger.LogDebug($"Provider for {source} not found");
      return new List<Measurement> {};
    }

    IndexLog(new Log(LogType.LoadBegin, provider.Source,
        new Log.KnownData { Period = period }));

        var searchDevicesResponse = SearchDevices(provider.Source);
        var devices = searchDevicesResponse.Sources();

        var measurements = new List<Measurement> {};

        foreach (var device in devices) {
          measurements.AddRange(
              await LoadDeviceMeasurementsAsync(device, period));
        }

        Logger.LogDebug($"Got {measurements.Count} measurements " +
                        $"from {provider.Source}");

        IndexLog(new Log(LogType.LoadEnd, provider.Source,
            new Log.KnownData { Period = period }));

        return measurements;
  }

  private async Task<Period> FetchLoadPeriodAsync(string source) {
    var lastLoadEndLog = (await SearchLoadLogsSortedByPeriodAsync(Source, 1))
                             .Sources()
                             .FirstOrDefault();
    var now = DateTime.UtcNow;

    if (lastLoadEndLog?.Data?.Period?.To is not null) {
      return new Period { From = lastLoadEndLog.Data.Period.To, To = now };
    }

    return new Period { From = now.Subtract(s_fallbackTimeSpan), To = now };
  }

  private static TimeSpan s_fallbackTimeSpan = TimeSpan.FromMinutes(5);
}
