using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OrchardCore.Navigation;

namespace Ozds.Modules.Ozds;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() =>
      string.Equals(
            name, "admin",
            StringComparison.OrdinalIgnoreCase) ?
        Env.IsDevelopment() ?
          BuildDevelopmentNavigation(builder)
        : BuildProductionNavigation(builder)
      : builder);


  public NavigationBuilder BuildDevelopmentNavigation(
      NavigationBuilder builder) =>
    builder
      .Add(
        S["OZDS"],
        S["OZDS"].PrefixPosition(),
        root => BuildOzdsMenu(root),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-users"
        })
      .Add(
        S["GraphQL"],
        S["GraphQL"].PrefixPosition(),
        root => root.Url("/graphql"),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-database"
        });

  public NavigationBuilder BuildProductionNavigation(
      NavigationBuilder builder) =>
    BuildOzdsMenu(builder);

  private NavigationBuilder BuildOzdsMenu(NavigationBuilder builder) =>
    builder
      .Add(
        S["Cjenik"],
        S["Cjenik"].PrefixPosition(),
        child => child
          .AddClass("ozds-admin-menu")
          .Action("List", "Admin",
            new
            {
              area = "OrchardCore.Contents",
              contentTypeId = "Catalogue"
            }),
          new[]
          {
            "icon-class-fas",
            "icon-class-fa-euro-sign"
          })
      .Add(
        S["ZDS"],
        S["ZDS"].PrefixPosition(),
        child => child
          .AddClass("ozds-admin-menu")
          .Action("List", "Admin",
            new
            {
              area = "OrchardCore.Contents",
              contentTypeId = "Center"
            }),
          new[]
          {
            "icon-class-fas",
            "icon-class-fa-bolt"
          })
      .Add(
        S["Korisnici ZDS-a"],
        S["Korisnici ZDS-a"].PrefixPosition(),
        child => child
          .AddClass("ozds-admin-menu")
          .Action("List", "Admin",
            new
            {
              area = "OrchardCore.Contents",
              contentTypeId = "Consumer"
            }),
          new[]
          {
            "icon-class-fas",
            "icon-class-fa-users"
          })
      .Add(
        S["OMM"],
        S["OMM"].PrefixPosition(),
        child => child
          .AddClass("ozds-admin-menu")
          .Action("List", "Admin",
            new
            {
              area = "OrchardCore.Contents",
              contentTypeId = "SecondarySite"
            }),
          new[]
          {
            "icon-class-fas",
            "icon-class-fa-tachometer"
          })
      .Add(
        S["Računi"],
        S["Računi"].PrefixPosition(),
        child => child
          .AddClass("ozds-admin-menu")
          .Action("List", "Admin",
            new
            {
              area = "OrchardCore.Contents",
              contentTypeId = "Receipt",
            }),
          new[]
          {
            "icon-class-fas",
            "icon-class-fa-receipt"
          });

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
