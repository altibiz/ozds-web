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
    services.AddSingleton<IMeasurementProvider, FakeMeasurementProvider>();

    services.AddSingleton<ElasticsearchMigratorAccessor>(
      services => new ElasticsearchMigratorAccessor());
    services.AddSingleton<
      IElasticsearchClientPrototype,
      ElasticsearchClient>();
    services.AddScoped<IElasticsearchClient>(services => services
      .GetRequiredService<IElasticsearchClientPrototype>()
      .ClonePrototype(
        new Indices(
          isDev: services
            .GetRequiredService<IHostEnvironment>()
            .IsDevelopment(),
          test: services
            .GetRequiredService<ITestOutputHelperAccessor>().Output
            ?.GetTest()
            ?.GetSpecificTestIndexSuffix() ??
              throw new InvalidOperationException(
                "Lacking test output"))));

    services.AddSingleton<
      Elasticsearch.HelbOzds.IClient,
      Elasticsearch.HelbOzds.Client>();

    services.AddSingleton<
      Elasticsearch.MyEnergyCommunity.IClient,
      Elasticsearch.MyEnergyCommunity.Client>();

    services.AddSingleton<FakeMeasurementProvider>();
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
