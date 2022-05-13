using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;

namespace Ozds.Modules.Members;

[BackgroundTask(
  Schedule = "*/1 * * * *",
  Description = "Imports measurements into Elasticsearch")]
public class MeasurementImporter : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    services
      .GetRequiredService<Elasticsearch.IMeasurementImporter>()
      .ImportMeasurementsAsync();
}
