using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ozds.Members.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Members.Pages {
  public class MyOfferModel : PageModel {
    private readonly IHtmlLocalizer H;
    private readonly MemberService _memberService;
    private readonly INotifier _notifier;
    public dynamic Shape { get; set; }

    public MyOfferModel(MemberService mService,
        IHtmlLocalizer<CreateMemberModel> htmlLocalizer, INotifier notifier) {
      _notifier = notifier;
      H = htmlLocalizer;
      _memberService = mService;
    }
    public async Task<IActionResult> OnGetAsync(string contentItemId = null) {
      if (contentItemId == null) {
        return RedirectToPage("OfferFor");
      }
      var offer = await _memberService.GetCompanyOffers(contentItemId, true);

      if (offer == null) {
        return RedirectToPage("CreateOffer", new { contentItemId });
      } else {
        if (!offer.Published) {
          await _notifier.InformationAsync(
              H["Molimo pri�ekajte da na�i administratori potvrde ponudu"]);
        } else {
          (Shape, _) = await _memberService.GetEditorById(offer.ContentItemId);
        }
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(string contentItemId) {
      var offer = await _memberService.GetCompanyOffers(contentItemId);

      ContentItem contentItem;
      (contentItem, Shape) =
          await _memberService.ModelToItem(offer.ContentItemId);

      if (ModelState.IsValid) {
        var result = await _memberService.UpdateContentItem(contentItem);

        if (result.Succeeded)
          await _notifier.SuccessAsync(H["Offer updated successful"]);
      }
      return Page();
    }
  }
}
