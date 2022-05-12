using System.Reflection;
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
using Lombiq.HelpfulExtensions.Extensions.CodeGeneration;
using Ozds.Modules.Members.Utils;
using Ozds.Modules.Members.PartFieldSettings;
using Ozds.Modules.Members.Base;
using Ozds.Elasticsearch;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Startup : OrchardCore.Modules.StartupBase
{
  public override void ConfigureServices(IServiceCollection services)
  {
    services.AddScoped<INavigationProvider, AdminMenu>();
    services.AddScoped<IDataMigration, Migrations>();

    services.AddContentPart<Tag>();
    services.AddContentPart<TariffTag>();

    services.AddContentPart<Person>();
    services.AddScoped<IScopedIndexProvider, PersonIndexProvider>();
    services.AddContentPart<Site>();
    services.AddContentPart<SecondarySite>();
    services.AddScoped<IScopedIndexProvider, SiteIndexProvider>();
    services.AddContentPart<Consumer>();
    services.AddScoped<ConsumerService>();
    services.AddContentPart<Center>();
    services.AddContentPart<ReceiptItem>();
    services.AddContentPart<Receipt>();
    services.AddScoped<IScopedIndexProvider, ReceiptIndexProvider>();
    services.AddScoped<IContentHandler, ReceiptHandler>();
    services.AddContentPart<Calculation>();
    services.AddContentPart<Catalogue>();
    services.AddContentPart<CatalogueItem>();
    services.AddContentPart<Expenditure>();
    services.AddContentPart<ExpenditureItem>();

    services.AddScoped<TaxonomyCacheService>();

    services.AddRecipeExecutionStep<FastImport>();
    services.AddScoped<Importer>();
    services.AddSingleton<IBackgroundTask, FastImportBackgroundTask>();

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
        Elasticsearch.MeasurementFaker.Client>();

      // NOTE: if a developer starts elasticsearch it would be better to work
      // NOTE: with that than fakes
      if (Elasticsearch.Client.Ping(Env, Conf))
      {
        services.AddSingleton<
          IMeasurementImporter,
          Elasticsearch.Client>();

        services.AddSingleton<
          IReceiptMeasurementProvider,
          Elasticsearch.Client>();
      }
      else
      {
        services.AddSingleton<
          IMeasurementImporter,
          Elasticsearch.FakeMeasurementImporter>();

        services.AddSingleton<
          IReceiptMeasurementProvider,
          Elasticsearch.FakeReceiptMeasurementProvider>();
      }
    }
    else
    {
      Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(type =>
          type.IsAssignableTo<IMeasurementProvider>() &&
          !type.IsInterface &&
          !type.Equals(typeof(Elasticsearch.Client)) &&
          // TODO: enable later on
          !type.Equals(typeof(Elasticsearch.HelbOzds.Client)) &&
          !type.Equals(typeof(Elasticsearch.MeasurementFaker.Client)))
        .ForEach(measurementProviderType =>
          services.AddSingleton(
            typeof(IMeasurementProvider),
            measurementProviderType))
        .Run();

      services.AddSingleton<
        IMeasurementImporter,
        Elasticsearch.Client>();

      services.AddSingleton<
        IReceiptMeasurementProvider,
        Elasticsearch.Client>();
    }

    services.AddSingleton<
      PeriodicMeasurementLoader>();
    services.AddSingleton<
      IBackgroundTask,
      PeriodicMeasurementLoadBackgroundTask>();

    services.AddScoped<LocalizedRouteTransformer>();
  }

  public override void Configure(
      IApplicationBuilder app,
      IEndpointRouteBuilder routes,
      IServiceProvider services)
  {
    routes.MapDynamicPageRoute<LocalizedRouteTransformer>("clanovi");
    routes.MapDynamicPageRoute<LocalizedRouteTransformer>("clanovi/{page?}");
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
}
