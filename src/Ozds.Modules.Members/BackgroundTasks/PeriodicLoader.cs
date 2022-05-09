using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.BackgroundTasks;

namespace Ozds.Modules.Members;

[BackgroundTask(
  Schedule = "*/1 * * * *",
  Description = "Loads measurements periodically")]
public class PeriodicMeasurementLoadBackgroundTask : IBackgroundTask
{
  public Task DoWorkAsync(
      IServiceProvider services,
      CancellationToken token) =>
    services
      .GetRequiredService<PeriodicMeasurementLoader>()
      .LoadContinuouslyAsync();
}

public class PeriodicMeasurementLoader
{
  public Task LoadContinuouslyAsync() =>
    Importer.ImportMeasurementsAsync();

  public PeriodicMeasurementLoader(IWebHostEnvironment env,
      ILogger<PeriodicMeasurementLoader> logger,
      Ozds.Elasticsearch.IMeasurementImporter importer)
  {
    Importer = importer;

    // if (env.IsDevelopment())
    // {
    //   Importer.IndexDevice(new Ozds.Elasticsearch.Device(
    //     Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource,
    //     Ozds.Elasticsearch.MeasurementFaker.Client.FakeDeviceId, null,
    //     Ozds.Elasticsearch.DeviceState.Healthy));
    //   logger.LogInformation("Indexed a fake device for development");
    // }
  }

  Ozds.Elasticsearch.IMeasurementImporter Importer { get; init; }
}
