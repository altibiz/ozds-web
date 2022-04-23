using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
  public async Task LoadContinuouslyAsync() =>
    await Client.IndexMeasurementsAsync(
      await Client.LoadMeasurementsAsync());

  public PeriodicMeasurementLoader(IWebHostEnvironment env,
      ILogger<PeriodicMeasurementLoader> logger,
      Ozds.Elasticsearch.IClient client)
  {
    Client = client;

    if (env.IsDevelopment())
    {
      Client.IndexDevice(new Ozds.Elasticsearch.Device(
        Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource,
        Ozds.Elasticsearch.MeasurementFaker.Client.FakeDeviceId, null,
        Ozds.Elasticsearch.DeviceState.Healthy));
      logger.Log(LogLevel.Information, "Indexed a fake device for development");
    }
  }

  Ozds.Elasticsearch.IClient Client { get; init; }
}
