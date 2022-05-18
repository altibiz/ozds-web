using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Modules.Members.Pages;

public class CreateNewModel : PageModel
{
  public async Task<IActionResult> OnGetAsync(string contentType)
  {
    (_, Shape) = await Members.GetNewItem(contentType);
    return Page();
  }

  // contentItemId -> company content item
  public async Task<IActionResult> OnPostAsync(string contentType)
  {
    ContentItem contentItem;
    (contentItem, Shape) = await Members.ModelToNew(contentType);
    if (ModelState.IsValid)
    {
      var result = await Members.CreateNew(contentItem, true);
      if (result.Succeeded)
      {
        await Notifier.SuccessAsync(H["Hvala!"]);
        return Redirect("~/Contents/ContentItems/" + contentItem.ContentItemId);
      }
    }
    return Page();
  }

  public IShape? Shape { get; set; }

  public CreateNewModel(
    ConsumerService mService,
    IHtmlLocalizer<CreateNewModel> htmlLocalizer,
    INotifier notifier)
  {
    Notifier = notifier;
    Members = mService;

    H = htmlLocalizer;
  }

  private IHtmlLocalizer H { get; }
  private ConsumerService Members { get; }
  private INotifier Notifier { get; }
}
