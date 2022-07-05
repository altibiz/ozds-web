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
    services.AddSingleton<
        IMeasurementProvider,
        FakeMeasurementProvider>();

    services.AddSingleton<
        IIndexNamer,
        IndexNamer>();
    services.AddSingleton<
        IIndexMapper,
        IndexMapper>();

    services.AddSingleton<
        IElasticsearchMigratorAccessor,
        ElasticsearchMigratorAccessor>();

    services.AddSingleton<
        IElasticsearchTestClientPrototype,
        ElasticsearchClient>();
    services.AddScoped<IElasticsearchClient>(services => services
      .GetRequiredService<IElasticsearchTestClientPrototype>()
      .MakeTestClient(services
        .GetRequiredService<IIndexNamer>()
        .MakeTestIndices(services
          .GetRequiredService<ITestOutputHelperAccessor>().Output
          .GetTest()
          .GetIndexSuffix() ??
            throw new InvalidOperationException(
              "Cannot get test index suffix"))));

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
