using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;
using OrchardCore.Data.Migration;
using OrchardCore.Navigation;

namespace Ozds.Themes.Ozds
{
  public class Startup : StartupBase
  {
    public override void ConfigureServices(
        IServiceCollection services)
    {
      services
          .AddTransient<IConfigureOptions<ResourceManagementOptions>,
              ResourceManagementOptionsConfiguration>();
      services.AddScoped<IDataMigration, Migrations>();
      services.AddScoped<INavigationProvider, AdminMenu>();
    }
  }
}
