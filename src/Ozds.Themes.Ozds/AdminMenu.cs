using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
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
          Env.IsDevelopment() &&
          (Conf
            .GetSection("Ozds")
            .GetSection("Modules")
            .GetSection("Ozds")
            .GetValue<object?>("IsDemo") is null),
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
