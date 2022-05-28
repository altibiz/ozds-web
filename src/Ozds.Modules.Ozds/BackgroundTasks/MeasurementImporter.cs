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
    Import(services, token);

  public Task Import(
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
                .ToValueTask())));
  }
}
