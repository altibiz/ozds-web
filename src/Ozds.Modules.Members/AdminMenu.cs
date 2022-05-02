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
          .Add(S["OZDS"], "0", root => root
            .Add(S["ZDS"], "0", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Center"
                }))
            .Add(S["Korisnici ZDS-a"], "1", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Consumer"
                }))
            .Add(S["OMM"], "2", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "SecondarySite"
                }))
            .Add(S["Računi"], "3", child => child
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
            })));

  public AdminMenu(IStringLocalizer<AdminMenu> localizer)
  {
    S = localizer;
  }

  private IStringLocalizer S { get; }
}
