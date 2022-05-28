using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using Ozds.Elasticsearch;
using Ozds.Util;

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
    await semaphoreSlim.WaitAsync();
    try
    {
      // NOTE: each plan is for one device which is safe to parallelize
      await Parallel.ForEachAsync(
        extractor.PlanExtractionAwait(period),
        token,
        (plan, token) => loader
          .LoadMeasurementsAwait(
            extractor
              .ExecuteExtractionPlanAsync(plan)
              .Enrich(measurement =>
                cache
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
                        data.Value.OwnerUserId))
                  .ToValueTask()))
          .ToValueTask());
    }
    finally
    {
      semaphoreSlim.Release();
    }
  }

  SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
}
