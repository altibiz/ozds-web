using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Ozds.Elasticsearch;

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
      period = await DetermineLoadPeriodAsync(source);
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

  private async Task<Period> DetermineLoadPeriodAsync(string source) {
    var lastLoadEndLogSearchResponse =
        await SearchLoadLogsSortedByPeriodAsync(source, 1);

    var lastLoadEndLog =
        lastLoadEndLogSearchResponse.Sources().FirstOrDefault();
    var lastLoadEnd = lastLoadEndLog?.Data?.Period?.To;

    var begin = lastLoadEnd;
    var end = DateTime.UtcNow;

    if (begin is null) {
      Logger.LogDebug($"Last load log not found for {source}");
      Logger.LogDebug(lastLoadEndLogSearchResponse.DebugInformation);
      begin = DateTime.MinValue.ToUniversalTime();
    }

    var period = new Period { From = (DateTime)begin, To = (DateTime)end };
    return period;
  }
}
