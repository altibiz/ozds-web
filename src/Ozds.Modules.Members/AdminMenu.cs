using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;
using OrchardCore.Navigation;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() =>
      name.When(
        name => string.Equals(
          name, "admin",
          StringComparison.OrdinalIgnoreCase),
        _ =>
          Env.IsDevelopment() ?
            BuildDevelopmentNavigation(builder)
            : BuildProductionNavigation(builder)));

  public NavigationBuilder BuildDevelopmentNavigation(
      NavigationBuilder builder) =>
    builder
      .Add(
        S["OZDS"],
        "0",
        root => root
          .Add(S["Cjenici"], "1", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Catalogue"
              }))
          .Add(S["ZDS"], "2", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Center"
              }))
          .Add(S["Korisnici ZDS-a"], "3", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Consumer"
              }))
          .Add(S["OMM"], "4", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "SecondarySite"
              }))
          .Add(S["Računi"], "5", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Receipt",
              })),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-users"
        });

  public NavigationBuilder BuildProductionNavigation(
      NavigationBuilder builder) =>
    builder
      .Remove(_ => true)
      .Add(S["Cjenici"], "1", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Catalogue"
          }))
      .Add(S["ZDS"], "2", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Center"
          }))
      .Add(S["Korisnici ZDS-a"], "3", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Consumer"
          }))
      .Add(S["OMM"], "4", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "SecondarySite"
          }))
      .Add(S["Računi"], "5", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Receipt",
          }));

  public AdminMenu(
      IStringLocalizer<AdminMenu> localizer,
      IHostEnvironment env)
  {
    S = localizer;
    Env = env;
  }

  private IStringLocalizer S { get; }
  private IHostEnvironment Env { get; }
}
