using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;

namespace Ozds.Modules.Ozds;

[BackgroundTask(
  Schedule = "*/5 * * * *",
  Description = "Measurement import background task.")]
public class MeasurementImportBackgroundTask : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    services
      .GetRequiredService<MeasurementImporter>()
      .ImportAsync(token: token);
}
