using Microsoft.Extensions.Logging;
using Ozds.Elasticsearch;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

// FIX: Collection was modified; enumeration operation may not execute.
public class MeasurementImporter
{
  public async Task ImportAsync(
    Period? period = null,
    CancellationToken? token = null)
  {
    await Semaphore.WaitAsync();
    try
    {
      if (await Extractor.CheckReadyAsync() is false)
      {
        Log.LogInformation(
          "Stopping measurement import because extractor is not ready");
        return;
      }

      var plans = Extractor.PlanExtractionAsync(period);

      if (token is null)
      {
        await Parallel
          .ForEachAsync(
            plans,
            (plan, token) => ImportPlanAsync(plan, token).ToValueTask());
      }
      else
      {
        await Parallel
          .ForEachAsync(
            plans,
            token.Value,
            (plan, token) => ImportPlanAsync(plan, token).ToValueTask());
      }
    }
    finally
    {
      Semaphore.Release();
    }
  }

  public Task ImportPlanAsync(
      ExtractionPlan plan,
      CancellationToken? token = null) =>
    Extractor
      .ExecuteExtractionPlanAsync(plan)
      .EnrichAwait(measurement => Cache
        .GetDeviceAsync(Device
          .MakeId(
            measurement.Source,
            measurement.SourceDeviceId))
        .ToValueTask()
        .Then(device =>
          device switch
          {
            null => measurement
              .ToLoadMeasurement(),
            not null => measurement
              .ToLoadMeasurement(
                device.Value.Operator,
                device.Value.CenterId,
                device.Value.CenterUserId,
                device.Value.OwnerId,
                device.Value.OwnerUserId)
          }))
      .Var(Loader.LoadMeasurementsAsync);

  public MeasurementImporter(
      ILogger<MeasurementImporter> log,

      IMeasurementExtractor extractor,
      IMeasurementLoader loader,
      MeasurementImportCache cache)
  {
    Log = log;

    Extractor = extractor;
    Loader = loader;
    Cache = cache;
  }

  ILogger Log { get; }

  IMeasurementExtractor Extractor { get; }
  IMeasurementLoader Loader { get; }
  MeasurementImportCache Cache { get; }

  SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1, 1);
}
