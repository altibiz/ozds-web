using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using OrchardCore.Data.Migration;
using OrchardCore.Navigation;
using OrchardCore.BackgroundTasks;
using OrchardCore.DisplayManagement.Implementation;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Drivers;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Taxonomies.Drivers;
using OrchardCore.Recipes;
using OrchardCore.Data;
using OrchardCore.Modules;
using Ozds.Modules.Ozds.PartFieldSettings;
using Ozds.Modules.Ozds.Base;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class Startup : OrchardCore.Modules.StartupBase
{
  public override void ConfigureServices(IServiceCollection services)
  {
    // NOTE: preventing from being instantiated twice
    services.AddScoped<TenantActivatedEvent>();
    services.AddScoped<IModularTenantEvents, TenantActivatedEvent>(
        s => s.GetRequiredService<TenantActivatedEvent>());
    services.AddSingleton<ITenantActivationChecker, TenantActivationChecker>();
    services.AddScoped<IDataMigration, Migrations>();

    services.AddContentPart<Tag>();
    services.AddContentPart<TariffTag>();

    services.AddContentPart<Person>();
    services.AddScoped<IScopedIndexProvider, PersonIndexProvider>();
    services.AddContentPart<Site>();
    services.AddScoped<IContentHandler, SiteEnricher>();
    services.AddScoped<IContentHandler, SiteDeviceLoader>();
    services.AddContentPart<SecondarySite>();
    services.AddScoped<IScopedIndexProvider, SiteIndexProvider>();
    services.AddContentPart<Consumer>();
    services.AddScoped<ConsumerService>();
    services.AddContentPart<Center>();
    services.AddContentPart<Catalogue>();
    services.AddContentPart<CatalogueItem>();
    services.AddContentPart<Receipt>();
    services.AddScoped<IContentHandler, ReceiptEnricher>();
    services.AddScoped<IScopedIndexProvider, ReceiptIndexProvider>();

    services.AddScoped<TaxonomyCacheService>();

    services.AddRecipeExecutionStep<FastImport>();
    services.AddScoped<Importer>();
    services.AddSingleton<IBackgroundTask, FastImportBackgroundTask>();

    services.AddRecipeExecutionStep<UserImport>();

    services.AddScoped<INavigationProvider, AdminMenu>();

    services.AddScoped<IContentDisplayDriver, ContainedPartDisplayDriver>();

    if (Env.IsDevelopment())
    {
      services.AddScoped<IShapeDisplayEvents, ShapeTracingShapeEvents>();
      services.AddScoped<IContentTypeDefinitionDisplayDriver,
          CodeGenerationDisplayDriver>();
    }

    services.AddContentField<TextField>()
        .ForEditor<TextFieldDisplayDriver>(d => false)
        .ForEditor<PartTextFieldDriver>(d => true);
    services.AddContentField<NumericField>()
        .ForEditor<NumericFieldDisplayDriver>(d => false)
        .ForEditor<PartNumericFieldDriver>(d => true);

    services.AddContentField<TaxonomyField>()
        .ForEditor<TaxonomyFieldTagsDisplayDriver>(d => false)
        .ForEditor<TaxonomyFieldDisplayDriver>(d =>
          !string.Equals(d, "Tags", StringComparison.OrdinalIgnoreCase) &&
          !string.Equals(d, "Disabled", StringComparison.OrdinalIgnoreCase))
        .ForEditor<PartTaxonomyFieldTagsDriver>(d =>
          string.Equals(d, "Tags", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(d, "Disabled", StringComparison.OrdinalIgnoreCase));

    if (Env.IsDevelopment())
    {
      services.AddSingleton<
        IMeasurementProvider,
        FakeMeasurementProvider>();

      services.AddSingleton<
        IMeasurementProvider,
        Elasticsearch.MyEnergyCommunity.Client>();

      // NOTE: if a developer starts elasticsearch it would be better to work
      // NOTE: with that than fakes
      if (Elasticsearch.ElasticsearchClient.Ping(Env, Conf))
      {
        AddElasticsearchClient(services);
      }
      else
      {
        services.AddSingleton<
          IDeviceLoader,
          Elasticsearch.FakeDeviceLoader>();

        services.AddSingleton<
          IMeasurementExtractor,
          Elasticsearch.FakeMeasurementExtractor>();

        services.AddSingleton<
          IMeasurementLoader,
          Elasticsearch.FakeMeasurementLoader>();

        services.AddSingleton<
          IReceiptMeasurementProvider,
          Elasticsearch.FakeReceiptMeasurementProvider>();

        services.AddSingleton<
          IDashboardMeasurementProvider,
          Elasticsearch.FakeDashboardMeasurementProvider>();
      }
    }
    else
    {
      services.AddSingleton<
        IMeasurementProvider,
        Elasticsearch.MyEnergyCommunity.Client>();

      AddElasticsearchClient(services);
    }

    services.AddSingleton<MeasurementImporterCache>();
    services.AddSingleton<MeasurementImporter>();
    services.AddSingleton<IBackgroundTask, MeasurementImporter>(
      services => services.GetRequiredService<MeasurementImporter>());

    services.AddScoped<LocalizedRouteTransformer>();

    services
      .AddGraphQLServer()
      .AddAuthorization()
      .AddQueryType<Query>()
      .AddType<MultiDashboardMeasurementsType>()
      .AddType<MultiDashboardMeasurementDataType>()
      .AddType<DeviceDashboardMeasurementDataType>()
      .AddType<DashboardMeasurementType>()
      .AddType<DashboardMeasurementDataType>()
      .AddType<PeriodType>();
  }

  public override void Configure(
      IApplicationBuilder app,
      IEndpointRouteBuilder routes,
      IServiceProvider services)
  {
    routes.MapDynamicPageRoute<LocalizedRouteTransformer>("");
    routes.MapDynamicPageRoute<LocalizedRouteTransformer>("korisnici");
    routes.MapDynamicPageRoute<LocalizedRouteTransformer>("korisnici/{page?}");

    app.UseEndpoints(endpoints => endpoints
      .MapGraphQL()
      .RequireAuthorization(
        new AuthorizeData
        {
          Roles = "Administrator,User"
        }));
  }

  public Startup(
      IWebHostEnvironment env,
      ILogger<Startup> logger,
      IConfiguration conf)
  {
    Env = env;
    Logger = logger;
    Conf = conf;
  }

  private IWebHostEnvironment Env { get; }
  private ILogger<Startup> Logger { get; }
  private IConfiguration Conf { get; }

  private void AddElasticsearchClient(IServiceCollection services)
  {
    // NOTE: this prevents the client from being instantiated multiple times
    services.AddSingleton<Elasticsearch.ElasticsearchClient>();
    services.AddSingleton<IDeviceLoader>(
        s => s.GetRequiredService<Elasticsearch.ElasticsearchClient>());
    services.AddSingleton<IMeasurementExtractor>(
        s => s.GetRequiredService<Elasticsearch.ElasticsearchClient>());
    services.AddSingleton<IMeasurementLoader>(
        s => s.GetRequiredService<Elasticsearch.ElasticsearchClient>());
    services.AddSingleton<IReceiptMeasurementProvider>(
        s => s.GetRequiredService<Elasticsearch.ElasticsearchClient>());
    services.AddSingleton<IDashboardMeasurementProvider>(
        s => s.GetRequiredService<Elasticsearch.ElasticsearchClient>());
  }
}
