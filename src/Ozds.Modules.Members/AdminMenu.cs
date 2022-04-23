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
          .Add(S["Članstvo"], "0", rootView => rootView
            .Add(S["ZDS"], "0", childTwo => childTwo
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Center"
                }))
            .Add(S["Članovi"], "1", childOne => childOne
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Member"
                })),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-users"
            })
          .Add(S["Financije"], "0", rootView => rootView
            .Add(S["Računi"], "0", childTwo => childTwo
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
