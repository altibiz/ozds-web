using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Members.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;

namespace Members.Pages
{
    public class MyOfferModel : PageModel
    {
        private readonly IHtmlLocalizer H;
        private readonly MemberService _memberService;
        private readonly INotifier _notifier;
        public dynamic Shape { get; set; }

        public MyOfferModel(MemberService mService, IHtmlLocalizer<CreateMemberModel> htmlLocalizer, INotifier notifier)
        {
            _notifier = notifier;
            H = htmlLocalizer;
            _memberService = mService;
        }

        public async Task<IActionResult> OnGetAsync(string offerId)
        {
            var offer = await _memberService.GetUserOffers();

            if (offer == null)
            {
               return  RedirectToPage("CreateOffer");
            }
            else
            {
                if (!offer.Published)
                {
                    _notifier.Information(H["Molimo pri�ekajte da na�i administratori potvrde ponudu"]);
                }
                else
                {
                    Shape = await _memberService.GetEditorById(offer.ContentItemId);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var offer = await _memberService.GetUserOffers();

            ContentItem contentItem;
            (contentItem, Shape) = await _memberService.GetUpdatedItem(offer.ContentItemId);

            if (ModelState.IsValid)
            {
                var result = await _memberService.UpdateContentItem(contentItem);

                if (result.Succeeded)
                    _notifier.Success(H["Offer updated successful"]);
            }
            return Page();
        }

    }
}
