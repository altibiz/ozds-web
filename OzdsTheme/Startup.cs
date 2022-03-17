using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;
using OrchardCore.Data.Migration;

namespace OrchardCore.Themes.OzdsTheme {
  public class Startup : StartupBase {
    public override void ConfigureServices(
        IServiceCollection serviceCollection) {
      serviceCollection
          .AddTransient<IConfigureOptions<ResourceManagementOptions>,
              ResourceManagementOptionsConfiguration>();
      serviceCollection.AddScoped<IDataMigration, Migrations>();
    }
  }
}
