using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Ozds.Elasticsearch.Test;

public class Startup
{
  public IHostBuilder CreateHostBuilder()
  {
    return Host.CreateDefaultBuilder();
  }

  public void ConfigureHost(IHostBuilder hostBuilder)
  {
    hostBuilder.ConfigureLogging(builder => builder.ClearProviders());
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddSingleton<IMeasurementProvider,
        Elasticsearch.MeasurementFaker.Client>();

    services.AddSingleton<
      Ozds.Elasticsearch.IClientPrototype,
      Ozds.Elasticsearch.Client>();
    services.AddScoped<Ozds.Elasticsearch.IClient>(s => s
      .GetRequiredService<Ozds.Elasticsearch.IClientPrototype>()
      .ClonePrototype(s
        .GetRequiredService<ITestOutputHelperAccessor>().Output
        ?.GetTest()
        ?.GetCorrespondingIndexName()));

    services.AddSingleton<
      Ozds.Elasticsearch.HelbOzds.IClient,
      Ozds.Elasticsearch.HelbOzds.Client>();

    services.AddSingleton<
      Ozds.Elasticsearch.MyEnergyCommunity.IClient,
      Ozds.Elasticsearch.MyEnergyCommunity.Client>();

    services.AddSingleton<
      Ozds.Elasticsearch.MeasurementFaker.IClient,
      Ozds.Elasticsearch.MeasurementFaker.Client>();
  }

  public void Configure(
      ILoggerFactory loggerFactory,
      ITestOutputHelperAccessor accessor)
  {
    // NOTE: this one doesn't respect IConfiguration
    loggerFactory.AddProvider(
      new XunitTestOutputLoggerProvider(
        accessor,
        (_, level) => level is >= LogLevel.Debug and < LogLevel.None));
  }
}
