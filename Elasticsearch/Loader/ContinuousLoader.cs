using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrchardCore.BackgroundTasks;

namespace Elasticsearch {
public static partial class Loader {
  public static void LoadContinuously(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator,
      Period? period = null) {
    var task = LoadContinuouslyAsync(
        elasticsearchClient, measurementProviderIterator, period);
    task.Wait();
  }

  public static async Task LoadContinuouslyAsync(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator,
      Period? period = null) {
    if (period == null) {
      period = await GetNextPeriodAsync(elasticsearchClient);
      if (period == null) {
        await LoadInitialAsync(
            elasticsearchClient, measurementProviderIterator);
        return;
      }
    }

    elasticsearchClient.IndexLoaderLog(
        new Log(LogType.LoadBegin, new Log.KnownData { Period = period }));

    var measurements = new List<Measurement> {};

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      foreach (var device in elasticsearchClient
                   .SearchDevices(measurementProvider.Source)
                   .Sources()) {
        var providerMeasurements =
            await measurementProvider.GetMeasurementsAsync(
                device, period.From, period.To);
        measurements.AddRange(providerMeasurements);
      }
    }

    elasticsearchClient.IndexLoaderLog(
        new Log(LogType.LoadEnd, new Log.KnownData { Period = period }));

    await elasticsearchClient.IndexMeasurementsAsync(measurements);
  }

  private static async Task<Period?> GetNextPeriodAsync(
      IClient elasticsearchClient) {
    var lastLoadEndLog =
        (await elasticsearchClient.SearchLoaderLogsSortedByPeriodAsync(
             LogType.LoadEnd, 1))
            .Sources()
            .FirstOrDefault();
    if (lastLoadEndLog == null || lastLoadEndLog.Data.Period == null) {
      return null;
    }

    return new Period { From = lastLoadEndLog.Data.Period.To,
      To = DateTime.Now };
  }
}

[BackgroundTask(Schedule = "*/5 * * * *",
    Description = "Continuously load measurement data.")]
public class ContinuousLoadBackgroundTask : IBackgroundTask {
  public async Task DoWorkAsync(
      IServiceProvider serviceProvider, CancellationToken cancellationToken) {
    await Loader.LoadContinuouslyAsync(
        _elasticsearchClient, _measurementProviderIterator);
  }

  public ContinuousLoadBackgroundTask(
      IMeasurementProviderIterator measurementProviderIterator,
      IClient elasticsearchClient) {
    _measurementProviderIterator = measurementProviderIterator;
    _elasticsearchClient = elasticsearchClient;
  }

  private IMeasurementProviderIterator _measurementProviderIterator;
  private IClient _elasticsearchClient;
}
}
