using System.Threading.Tasks;
using Ozds.Members.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Members.Pages
{
  public class CreateOfferModel : PageModel
  {
    private readonly IHtmlLocalizer H;
    private readonly MemberService _memberService;
    private readonly INotifier _notifier;
    public IShape Shape { get; set; }
    public CreateOfferModel(MemberService mService,
        IHtmlLocalizer<CreateOfferModel> htmlLocalizer, INotifier notifier)
    {

      _notifier = notifier;
      H = htmlLocalizer;
      _memberService = mService;
    }

    public async Task<IActionResult> OnGetAsync(string contentItemId)
    {
      var offer = await _memberService.GetCompanyOffers(contentItemId);

      if (offer != null)
        return RedirectToPage("MyOffer");
      (_, Shape) = await _memberService.GetNewItem(ContentType.Offer);
      return Page();
    }

    // contentItemId -> company content item
    public async Task<IActionResult> OnPostCreateAsync(string contentItemId)
    {
      return await CreatePOST("Portal", contentItemId);
    }

    // contentItemId -> company content item
    private async Task<IActionResult> CreatePOST(
        string nextPage, string contentItemId)
    {
      ContentItem contentItem;
      (contentItem, Shape) = await _memberService.ModelToNew(ContentType.Offer);
      if (ModelState.IsValid)
      {
        var result =
            await _memberService.CreateOfferDraft(contentItem, contentItemId);
        if (result.Succeeded)
        {
          await _notifier.SuccessAsync(H["Offer created successful"]);
          return RedirectToPage(nextPage);
        }
      }
      return Page();
    }
  }
}
