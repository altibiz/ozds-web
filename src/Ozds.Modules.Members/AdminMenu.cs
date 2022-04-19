using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace Ozds.Modules.Members
{
  public class AdminMenu : INavigationProvider
  {
    private readonly IStringLocalizer S;

    public AdminMenu(IStringLocalizer<AdminMenu> localizer) { S = localizer; }

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
      // We want to add our menus to the "admin" menu only.
      if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
      {
        return Task.CompletedTask;
      }

      // Adding our menu items to the builder.
      // The builder represents the full admin menu tree.
      builder
          .Add(S["Članstvo"], "0",
              rootView => rootView
                              .Add(S["Clanovi"], "0",
                                  childOne => childOne.Action("List", "Admin",
                                      new
                                      {
                                        area = "OrchardCore.Contents",
                                        contentTypeId = "Member"
                                      }))
                              .Add(S["ZDS"], "1",
                                  childTwo => childTwo.Action("List", "Admin",
                                      new
                                      {
                                        area = "OrchardCore.Contents",
                                        contentTypeId = "Center"
                                      }))
                              .Add(S["OMM"], "2",
                                  childTwo => childTwo.Action("List", "Admin",
                                      new
                                      {
                                        area = "OrchardCore.Contents",
                                        contentTypeId = "Site"
                                      })),
              new[] { "icon-class-fas", "icon-class-fa-users" })
          .Add(S["Financije"], "0",
              rootView =>
                  rootView
                      .Add(S["Obracuni"], "0",
                          childTwo => childTwo.Action("List", "Admin",
                              new
                              {
                                area = "OrchardCore.Contents",
                                contentTypeId = "Calculation"
                              }))
                      .Add(S["Racuni"], "1",
                          childTwo => childTwo.Action("List", "Admin",
                              new
                              {
                                area = "OrchardCore.Contents",
                                contentTypeId = "Receipt",
                                q = "payout:true"
                              })),
              new[] { "icon-class-fas", "icon-class-fa-coins" });

      return Task.CompletedTask;
    }
  }
}
