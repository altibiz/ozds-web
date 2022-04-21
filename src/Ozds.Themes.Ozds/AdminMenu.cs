using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using OrchardCore.Navigation;
using Ozds.Util;

namespace Ozds.Themes.Ozds;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    name
      .When(name =>
        string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase),
        () => builder
          .Add(S["Content"], "1", root => root
            .Add(S["Content Items"], "0", child => child
              .Action("ContentItems", AdminController,
                new
                {
                  area = "OrchardCore.Contents"
                }))))
      .Return(Task.CompletedTask);

  public AdminMenu(
      IStringLocalizer<AdminMenu> localizer,
      IConfiguration conf)
  {
    S = localizer;
    Conf = conf;

    AdminController =
      Conf
        .GetSection("OrchardCore_Admin")
        .GetValue<string>("AdminUrlPrefix")
      ?? "Admin";
  }

  private IStringLocalizer S { get; }
  private IConfiguration Conf { get; }

  private string AdminController { get; }
}
