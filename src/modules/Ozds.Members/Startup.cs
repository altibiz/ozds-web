using System;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using YesSql.Indexes;
using OrchardCore.Data.Migration;
using OrchardCore.Navigation;
using OrchardCore.Contents.Services;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Contents.ViewModels;
using OrchardCore.BackgroundTasks;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DisplayManagement.Implementation;
using OrchardCore.ContentManagement;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Drivers;
using OrchardCore.Data;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Taxonomies.Drivers;
using OrchardCore.Recipes;
using Lombiq.HelpfulExtensions.Extensions.CodeGeneration;
using Ozds.Members.Utils;
using Ozds.Members.Core;
using Ozds.Members.Persons;
using Ozds.Members.PartFieldSettings;
using Ozds.Members.Payments;
using Ozds.Members.Indexes;
using Ozds.Members.Base;
using Ozds.Members.ContentHandlers;
using Ozds.Members.Measurements;

namespace Ozds.Members;

public class Startup : OrchardCore.Modules.StartupBase {
  public IWebHostEnvironment Env { get; init; }
  public ILogger<Startup> Logger { get; init; }

  public Startup(IWebHostEnvironment env, ILogger<Startup> logger) {
    Env = env;
    Logger = logger;
  }

  public override void ConfigureServices(IServiceCollection services) {
    services.AddScoped<INavigationProvider, AdminMenu>();
    services.AddScoped<IDataMigration, Migrations>();
    services.AddContentPart<Member>();
    services.AddContentPart<Company>();
    services.UsePartService<PersonPart, PersonPartService>();
    services.UsePartService<BankStatPart, BankStatPartService>();
    services.AddScoped<MemberService>();
    services.AddScoped<PaymentUtils>();
    services.AddScoped<IScopedIndexProvider, PersonPartIndexProvider>();
    services.AddSingleton<IIndexProvider, PaymentIndexProvider>();
    services.AddSingleton<IIndexProvider, OfferIndexProvider>();
    services.AddSingleton<IIndexProvider, PaymentByDayIndexProvider>();
    services.AddContentPart<Payment>();
    services.AddContentPart<Offer>();
    services.AddScoped<TaxonomyCachedService>();
    services.AddSingleton<IContentHandler, MemberHandler>();
    services.AddSingleton<IContentHandler, UserMenuHandler>();
    services.AddRecipeExecutionStep<FastImport>();
    services.AddScoped<Importer>();
    services.AddTransient<IContentsAdminListFilterProvider,
        PersonPartAdminListFilterProvider>();
    services.AddTransient<IContentsAdminListFilterProvider,
        PaymentAdminListFilterProvider>();
    services.AddScoped<IDisplayDriver<ContentOptionsViewModel>,
        PersonOptionsDisplayDriver>();
    services.UsePartService<Pledge, PledgeService>();
    services.UsePartService<Payment, PaymentPartService>();

    services.AddScoped<IContentDisplayDriver, ContainedPartDisplayDriver>();
    services.AddSingleton<IBackgroundTask, FastImportBackgroundTask>();

    if (Env.IsDevelopment()) {
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
        .ForEditor<TaxonomyFieldDisplayDriver>(
            d =>
                !string.Equals(d, "Tags", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(
                    d, "Disabled", StringComparison.OrdinalIgnoreCase))
        .ForEditor<PartTaxonomyFieldTagsDriver>(d => {
          return string.Equals(d, "Tags", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(
                     d, "Disabled", StringComparison.OrdinalIgnoreCase);
        });

    if (Env.IsDevelopment()) {
      services.AddSingleton<Ozds.Elasticsearch.IMeasurementProvider,
          Ozds.Elasticsearch.MeasurementFaker.Client>();
    } else {
      foreach (var measurementProviderType in Assembly.GetExecutingAssembly()
                   .GetTypes()
                   .Where(type =>
                              typeof(Ozds.Elasticsearch.IMeasurementProvider)
                                  .IsAssignableFrom(type) &&
                              !type.IsInterface &&
                              !type.Equals(typeof(Ozds.Elasticsearch.Client)) &&
                              !type.Equals(
                                  typeof(Ozds.Elasticsearch.MeasurementFaker
                                             .Client)))) {
        services.AddSingleton(typeof(Ozds.Elasticsearch.IMeasurementProvider),
            measurementProviderType);
      }
    }

    services
        .AddSingleton<Ozds.Elasticsearch.IClient, Ozds.Elasticsearch.Client>();
        services.AddSingleton<PeriodicMeasurementLoader>();
        services.AddSingleton<IBackgroundTask,
            PeriodicMeasurementLoadBackgroundTask>();
  }
}
