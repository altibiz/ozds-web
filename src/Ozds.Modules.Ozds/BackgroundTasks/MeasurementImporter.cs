using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

[BackgroundTask(
  Schedule = "*/1 * * * *",
  Description = "Imports measurements into Elasticsearch")]
public class MeasurementImporter : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
   services
     .GetRequiredService<IMeasurementExtractor>()
     .PlanExtractionAwait()
     .ForEachAwaitAsync(plan => services
      .GetRequiredService<IMeasurementLoader>()
      .LoadMeasurementsAwait(services
        .GetRequiredService<IMeasurementExtractor>()
        .ExecuteExtractionPlanAsync(plan)
        .Items.Select(item =>
          {
            // TODO: log missing and load here

            return new ExtractionBucket<LoadMeasurement>(
              item.Original.Period,
              item.Bucket
                .Select(measurement =>
                  {
                    // TODO: load/cache operator/center/owner here

                    return measurement
                      .ToLoadMeasurement(
                        "",
                        "",
                        "",
                        "",
                        "");
                  }));
          })));
}
