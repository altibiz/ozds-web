using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.DisplayManagement.Notify;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Alias.Models;
using OrchardCore.Menu.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Admin;
using Microsoft.AspNetCore.Http;

namespace Ozds.Modules.Members.Pages
{
  [Authorize]
  public class IndexModel : PageModel
  {
    public ContentItem? Member { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
      Member = await Members.GetUserMember(true);
      if (Member is null)
      {
        return RedirectToPage("CreateMember");
      }
      if (!Member.Published)
      {
        await Notifier.InformationAsync(
            H["Molimo pričekajte da naši administratori potvrde prijavu"]);
      }

      return Page();
    }

    public IndexModel(ConsumerService members, INotifier notifier,
        IHtmlLocalizer<IndexModel> localizer)
    {
      Members = members;
      Notifier = notifier;
      H = localizer;
    }

    private ConsumerService Members { get; }
    private INotifier Notifier { get; }
    private IHtmlLocalizer<IndexModel> H { get; }
  }
}

namespace Ozds.Modules.Members.ContentHandlers
{
  public class LinkMenuItem : ContentPart
  {
    public TextField Icon { get; set; } = default!;
  }

  public class MenuItem
  {
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Text { get; set; } = default!;
  }

  public class UserMenuHandler : ContentHandlerBase
  {
    public static MenuItem[] Menu { get; } =
      new[]
      {
        new MenuItem
        {
          Name = "Moji podaci",
          Url = "/Ozds.Modules.Members/MyProfile",
          Text = "far fa-address-card",
        },
      };

    public static IEnumerable<ContentItem> GetMenuContent()
    {
      return Menu.Select(x =>
      {
        var item = new ContentItem
        {
          ContentItemId = Guid.NewGuid().ToString(),
          Published = true,
          ContentType = "LinkMenuItem",
          DisplayText = x.Name,
        };
        item.Apply(new LinkMenuItemPart { Url = x.Url });
        item.Apply(new LinkMenuItem { Icon = new TextField { Text = x.Text } });
        return item;
      });
    }

    public override Task LoadedAsync(LoadContentContext context)
    {
      if (context.ContentItem.ContentType == "Menu" &&
          !AdminAttribute.IsApplied(Http.HttpContext))
      {
        var alias = ContentItemExtensions.As<AliasPart>(context.ContentItem);
        if (alias != null)
        {
          if (alias.Alias == "user-landing-page-menu")
          {
            var menulist = ContentItemExtensions.As<MenuItemsListPart>(
                context.ContentItem);
            menulist.MenuItems =
                GetMenuContent().Concat(menulist.MenuItems).ToList();
          }
        }
      }
      return Task.CompletedTask;
    }

    public UserMenuHandler(IHttpContextAccessor http) { Http = http; }

    private IHttpContextAccessor Http { get; }
  }
}
