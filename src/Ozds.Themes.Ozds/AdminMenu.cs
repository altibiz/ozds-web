using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using Ozds.Util;

namespace Ozds.Themes.Ozds;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() => name
      .WhenWith(name =>
        name.Equals("admin", StringComparison.OrdinalIgnoreCase),
        name => builder
          .Add(S["Content"], "1", root => root
            .Add(S["Content Items"], "0", child => child
              .Action("ContentItems", "Admin",
                new
                {
                  area = "OrchardCore.Contents"
                }))
            .Add(S["Taxonomies"], "1", child => child
              .Action("List", "Admin",
                new
                {
                  area = "OrchardCore.Contents",
                  contentTypeId = "Taxonomy"
                })))));

  public AdminMenu(IStringLocalizer<AdminMenu> localizer)
  {
    S = localizer;
  }

  private IStringLocalizer S { get; }
}
