using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Elasticsearch.Test;

public class Startup {
  public IHostBuilder CreateHostBuilder() {
    return Host.CreateDefaultBuilder();
  }

  public void ConfigureHost(IHostBuilder hostBuilder) {
    hostBuilder.ConfigureLogging(builder => builder.ClearProviders());
  }

  public void ConfigureServices(IServiceCollection services) {
    services
        .AddSingleton<Elasticsearch.IClientPrototype, Elasticsearch.Client>();
    services.AddScoped<Elasticsearch.IClient>(
        s => s.GetRequiredService<Elasticsearch.IClientPrototype>()
                 .ClonePrototype(
                     s.GetRequiredService<ITestOutputHelperAccessor>()
                         .Output?.GetTest()
                         ?.GetCorrespondingIndexName()));

    services.AddSingleton<Elasticsearch.HelbOzds.IClient,
        Elasticsearch.HelbOzds.Client>();
        services.AddSingleton<Elasticsearch.MyEnergyCommunity.IClient,
            Elasticsearch.MyEnergyCommunity.Client>();
        services.AddSingleton<Elasticsearch.MeasurementFaker.IClient,
            Elasticsearch.MeasurementFaker.Client>();

        services.AddSingleton<Elasticsearch.IMeasurementProviderIterator,
            Elasticsearch.FakeMeasurementProviderIterator>();
  }

  public void Configure(
      ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) {
    loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
  }
}
