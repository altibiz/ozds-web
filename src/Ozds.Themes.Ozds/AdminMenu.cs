using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;
using OrchardCore.Navigation;
using Ozds.Util;

namespace Ozds.Themes.Ozds;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() => name
      .When(
        name =>
          string.Equals(
            name, "admin",
            StringComparison.OrdinalIgnoreCase) &&
          Env.IsDevelopment(),
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
