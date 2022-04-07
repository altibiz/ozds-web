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
  public class OfferForModel : PageModel {
    private readonly MemberService _memberService;
    public List<ContentItem> CompanyContentItems { get; set; }

    public OfferForModel(MemberService mService) { _memberService = mService; }

    public async Task<IActionResult> OnGetAsync() {
      CompanyContentItems = await _memberService.GetUserCompanies();
      if (CompanyContentItems.Count == 1)
        return RedirectToPage("MyOffer",
            new { contentItemId = CompanyContentItems[0].ContentItemId });
      return Page();
    }
  }
}
