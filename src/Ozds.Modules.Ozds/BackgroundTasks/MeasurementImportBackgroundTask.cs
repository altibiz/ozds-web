using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;

namespace Ozds.Modules.Ozds;

// FIX: Collection was modified; enumeration operation may not execute.
public class MeasurementImportBackgroundTask : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    services
      .GetRequiredService<MeasurementImporter>()
      .ImportAsync(token: token);
}
