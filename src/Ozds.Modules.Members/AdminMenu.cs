using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() =>
      name.WhenWith(name => string
        .Equals(name, "admin", StringComparison.OrdinalIgnoreCase),
        name => builder
          .Add(S["Članstvo"], "0", root => root
            .Add(S["Zatvoreni distribucijski sustavi"], "0", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Center"
                }))
            .Add(S["Članovi"], "1", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Member"
                }))
            .Add(S["Obračunska mjerna mjesta"], "2", child => child
              .Add(S["Primarna"], "0", child => child
                .Action("List", "Admin",
                  new
                  {
                    area = "OrchardCore.Contents",
                    contentTypeId = "PrimarySite"
                  }))
              .Add(S["Sekundarna"], "1", child => child
                .Action("List", "Admin",
                  new
                  {
                    area = "OrchardCore.Contents",
                    contentTypeId = "SecondarySite"
                  }))),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-users"
            })
          .Add(S["Financije"], "0", root => root
            .Add(S["Katalozi"], "0", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Catalogue",
                }))
            .Add(S["Ugovori"], "1", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Contract",
                }))
            .Add(S["Računi"], "2", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Receipt",
                })),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-coins"
            })));

  public AdminMenu(IStringLocalizer<AdminMenu> localizer)
  {
    S = localizer;
  }

  private IStringLocalizer S { get; }
}
