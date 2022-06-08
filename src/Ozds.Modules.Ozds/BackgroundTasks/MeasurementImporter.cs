using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using Ozds.Elasticsearch;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

// FIX: Collection was modified; enumeration operation may not execute.
[BackgroundTask(
  Schedule = "*/1 * * * *",
  Description = "Imports measurements into Elasticsearch")]
public class MeasurementImporter : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    Import(services, token);

  public async Task Import(
    IServiceProvider services,
    CancellationToken token,
    Period? period = null)
  {
    var extractor = services.GetRequiredService<IMeasurementExtractor>();
    var loader = services.GetRequiredService<IMeasurementLoader>();
    var cache = services.GetRequiredService<MeasurementImporterCache>();

    // NOTE: https://stackoverflow.com/a/45769160/4348107
    // NOTE: we don't want to cause any race conditions here
    // TODO: optimize locking strategy per device
    await semaphore.WaitAsync();
    try
    {
      // NOTE: each plan is for one device which is safe to parallelize
      await Parallel.ForEachAsync(
        extractor.PlanExtractionAsync(period),
        token,
        async (plan, token) => await loader
          .LoadMeasurementsAsync(
            extractor
              .ExecuteExtractionPlanAsync(plan)
              .EnrichAwait(async measurement => await cache
                .GetDeviceData(
                  Device.MakeId(
                    measurement.Source,
                    measurement.SourceDeviceId))
                .Then(data =>
                  data is null ? measurement.ToLoadMeasurement()
                  : measurement
                    .ToLoadMeasurement(
                      data.Value.Operator,
                      data.Value.CenterId,
                      data.Value.CenterUserId,
                      data.Value.OwnerId,
                      data.Value.OwnerUserId)))));
    }
    finally
    {
      semaphore.Release();
    }
  }

  SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
}
