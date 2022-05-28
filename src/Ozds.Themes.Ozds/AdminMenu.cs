using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OrchardCore.Navigation;
using OrchardCore.Users.Models;
using DeploymentPermissions = OrchardCore.Deployment.Permissions;
using UserPermissions = OrchardCore.Users.Permissions;

namespace Ozds.Themes.Ozds;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() =>
      string.Equals(
          name, "admin",
          StringComparison.OrdinalIgnoreCase) &&
        Conf
          .GetSection("Ozds")
          .GetSection("Modules")
          .GetSection("Ozds")
          .GetValue<object?>("IsDemo") is not null ?
        builder
          .Add(
            S["Security"],
            NavigationConstants.AdminMenuSecurityPosition,
            security => security
              .AddClass("security")
              .Id("security")
              .Add(
                S["Users"],
                S["Users"].PrefixPosition(),
                users => users
                  .AddClass("users")
                  .Id("users")
                  .Action("Index", "Admin", "OrchardCore.Users")
                  .Permission(UserPermissions.ViewUsers)
                  .Resource(new User())
                  .LocalNav()))
          .Add(
            S["Import/Export"],
            S["Import/Export"].PrefixPosition(),
            import => import
              .Add(
                S["Deployment Plans"],
                S["Deployment Plans"].PrefixPosition(),
                deployment => deployment
                  .Action(
                    "Index",
                    "DeploymentPlan",
                    new
                    {
                      area = "OrchardCore.Deployment"
                    })
                  .Permission(DeploymentPermissions.Export)
                  .LocalNav())
              .Add(
                S["Package Import"],
                S["Package Import"].PrefixPosition(),
                deployment => deployment
                  .Action(
                    "Index",
                    "Import",
                    new
                    {
                      area = "OrchardCore.Deployment"
                    })
                  .Permission(DeploymentPermissions.Import)
                  .LocalNav())
              .Add(
                S["JSON Import"],
                S["JSON Import"].PrefixPosition(),
                deployment => deployment
                  .Action(
                    "Json",
                    "Import",
                    new
                    {
                      area = "OrchardCore.Deployment"
                    })
                  .Permission(DeploymentPermissions.Import)
                  .LocalNav()))
      : string.Equals(
          name, "admin",
          StringComparison.OrdinalIgnoreCase) &&
        Env.IsDevelopment() ?
        builder
          .Add(
            S["Content"],
            S["Content"].PrefixPosition(),
            root => root
              .Add(
                S["Content Items"],
                S["Content Items"].PrefixPosition(),
                child => child
                  .Action("ContentItems", "Admin",
                    new
                    {
                      area = "OrchardCore.Contents"
                    }))
            .Add(
              S["Taxonomies"],
              S["Taxonomies"].PrefixPosition(),
              child => child
                .Action("List", "Admin",
                  new
                  {
                    area = "OrchardCore.Contents",
                    contentTypeId = "Taxonomy"
                  })))
      : builder);

  public AdminMenu(
      IHostEnvironment env,
      IConfiguration conf,
      IStringLocalizer<AdminMenu> localizer)
  {
    Env = env;
    Conf = conf;

    S = localizer;
  }

  private IHostEnvironment Env { get; }
  private IConfiguration Conf { get; }

  private IStringLocalizer S { get; }
}
