using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;

namespace Ozds.Modules.Ozds;

[BackgroundTask(
  Schedule = "*/1 * * * *",
  Description = "Fast import background task.")]
public class FastImportBackgroundTask : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    services
      .GetRequiredService<FastImporter>()
      .ImportAsync();
}
