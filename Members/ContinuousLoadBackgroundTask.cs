using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using OrchardCore.BackgroundTasks;

namespace Members;

[BackgroundTask(
    Schedule = "*/1 * * * *", Description = "Loads seasurements periodically")]
public class ContinuousLoadBackgroundTask : IBackgroundTask {
  public async Task DoWorkAsync(
      IServiceProvider services, CancellationToken token) =>
      await LoadContinuously(
          services.GetRequiredService<Elasticsearch.IClient>(),
          services.GetRequiredService<
              Elasticsearch.IMeasurementProviderIterator>());

  private async Task LoadContinuously(Elasticsearch.IClient client,
      Elasticsearch.IMeasurementProviderIterator providers) {
    await client.LoadContinuouslyAsync(providers);
  }
}
