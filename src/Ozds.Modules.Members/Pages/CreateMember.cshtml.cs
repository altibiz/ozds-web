using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Modules.Members.Pages
{
  [Authorize]
  public class CreateMemberModel : PageModel
  {

    public async Task<IActionResult> OnGetAsync()
    {
      var mbr = await Members.GetUserMember(true);
      if (mbr != null)
        return RedirectToPage("Portal");
      (_, Shape) = await Members.GetNewItem(ContentType.Member);
      return Page();
    }

    public async Task<IActionResult> OnPostCreateMemberAsync()
    {
      return await CreatePOST("Portal");
    }
    public async Task<IActionResult> OnPostCreateMemberToCompanyAsync()
    {
      return await CreatePOST("CreateCompany");
    }

    private async Task<IActionResult> CreatePOST(string nextPage)
    {
      ContentItem contentItem;
      (contentItem, Shape) =
          await Members.ModelToNew(ContentType.Member);
      if (ModelState.IsValid)
      {
        var result = await Members.CreateMemberDraft(contentItem);
        if (result.Succeeded)
        {
          await Notifier.SuccessAsync(H["Member registration successful"]);
          return RedirectToPage(nextPage);
        }
      }
      return Page();
    }

    public IShape? Shape { get; private set; }

    public CreateMemberModel(
        IHtmlLocalizer<CreateMemberModel> localizer,
        MemberService members,
        INotifier notifier)
    {
      H = localizer;

      Members = members;
      Notifier = notifier;
    }

    private readonly IHtmlLocalizer H;
    private readonly MemberService Members;
    private readonly INotifier Notifier;
  }
}
