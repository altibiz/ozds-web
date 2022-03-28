using System;
using System.Threading;
using System.Threading.Tasks;
using OrchardCore.BackgroundTasks;

namespace Elasticsearch {
[BackgroundTask(Schedule = "*/5 * * * *",
    Description = "Continuously load measurement data.")]
public class ContinuousLoadBackgroundTask : IBackgroundTask {
  public async Task DoWorkAsync(
      IServiceProvider serviceProvider, CancellationToken cancellationToken) {
    await _elasticsearchClient.LoadContinuouslyAsync(
        _measurementProviderIterator);
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
