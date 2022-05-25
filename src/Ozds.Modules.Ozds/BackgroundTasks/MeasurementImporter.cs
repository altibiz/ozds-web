using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using Ozds.Elasticsearch;
using Ozds.Util;

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
        .Items.SelectAwait(async item =>
          {
            // TODO: log missing/invalid/duplicate and load here

            return new ExtractionBucket<LoadMeasurement>(
              item.Original.Period,
              await item.Bucket
                .Select(measurement =>
                  services
                    .GetRequiredService<MeasurementImporterCache>()
                    .GetDeviceData(
                      Device.MakeId(
                        measurement.Source,
                        measurement.SourceDeviceId))
                    .Then(data => measurement
                      .ToLoadMeasurement(
                        data.Operator,
                        data.CenterId,
                        data.CenterUserId,
                        data.OwnerId,
                        data.OwnerUserId)))
                .Await());
          })));
}
