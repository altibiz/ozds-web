using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OrchardCore.BackgroundTasks;

namespace Members.Measurements;

public class PeriodicMeasurementLoader
{
  public PeriodicMeasurementLoader(IWebHostEnvironment env,
      ILogger<PeriodicMeasurementLoader> logger, Elasticsearch.IClient client)
  {
    Client = client;

    if (env.IsDevelopment())
    {
      Client.IndexDevice(new Elasticsearch.Device(
          Elasticsearch.MeasurementFaker.Client.FakeSource,
          Elasticsearch.MeasurementFaker.Client.FakeDeviceId, null,
          Elasticsearch.DeviceState.Healthy));
      logger.Log(LogLevel.Information, "Indexed a fake device for development");
    }
  }

  public async Task LoadContinuouslyAsync()
  {
    await Client.IndexMeasurementsAsync(await Client.LoadMeasurementsAsync());
  }

  Elasticsearch.IClient Client { get; init; }
}

[BackgroundTask(
    Schedule = "*/1 * * * *", Description = "Loads measurements periodically")]
public class PeriodicMeasurementLoadBackgroundTask : IBackgroundTask
{
  public Task DoWorkAsync(IServiceProvider services, CancellationToken token) =>
      services.GetRequiredService<PeriodicMeasurementLoader>()
          .LoadContinuouslyAsync();
}