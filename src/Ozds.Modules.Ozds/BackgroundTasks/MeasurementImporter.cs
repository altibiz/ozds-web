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
      CancellationToken token)
  {
    var extractor = services.GetRequiredService<IMeasurementExtractor>();
    var loader = services.GetRequiredService<IMeasurementLoader>();
    var cache = services.GetRequiredService<MeasurementImporterCache>();

    return extractor
      .PlanExtractionAwait()
      .ForEachAwaitAsync(plan => loader
        .LoadMeasurementsAwait(
          new EnrichedMeasurementExtractionAsync
          {
            Device = plan.Device,
            Items =
              extractor
                .ExecuteExtractionPlanAsync(plan)
                .Items.SelectAwait<MeasurementExtractionItem,
                EnrichedMeasurementExtractionItem>(item => item.Bucket
                  .Select(measurement =>
                    cache
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
                  .AwaitValueTask()
                  .Then(items =>
                    new EnrichedMeasurementExtractionItem
                    {
                      Original = item.Original,
                      Next = item.Next,
                      Bucket =
                        new ExtractionBucket<LoadMeasurement>(
                          item.Original.Period,
                          items)
                    }))
          }));
  }
}
