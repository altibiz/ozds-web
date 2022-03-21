using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrchardCore.BackgroundTasks;

namespace Elasticsearch {
public static partial class Loader {
  public static async Task LoadContinuouslyAsync(IClient elasticsearchClient,
      IMeasurementProviderIterator measurementProviderIterator) {
    var period = await GetNextPeriodAsync(elasticsearchClient);
    if (period == null) {
      await LoadInitialAsync(elasticsearchClient, measurementProviderIterator);
      return;
    }

    elasticsearchClient.addLoaderLog(new Log { timestamp = DateTime.Now,
      type = LogType.LoadBegin, period = period });

    var measurements = new List<Measurement> {};

    foreach (var measurementProvider in measurementProviderIterator.Iterate()) {
      var providerMeasurements =
          await measurementProvider.getMeasurementsSortedAsync(
              "", "", period.from, period.from);

      measurements.AddRange(providerMeasurements);
    }

    elasticsearchClient.addLoaderLog(
        new Log { timestamp = DateTime.Now, type = LogType.LoadEnd });

    await elasticsearchClient.addMeasurementsAsync(measurements);
  }

  private static async Task<Period?> GetNextPeriodAsync(
      IClient elasticsearchClient) {
    var lastPeriodEnd =
        (await elasticsearchClient.getLoaderLogsSortedAsync(LogType.LoadEnd, 1))
            .ToList()[0];
    if (lastPeriodEnd == null) {
      return null;
    }

    return new Period { from = lastPeriodEnd.timestamp, to = DateTime.Now };
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
