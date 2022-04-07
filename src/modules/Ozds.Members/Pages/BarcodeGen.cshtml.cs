using System.Collections.Generic;
using System.Threading.Tasks;
using Ozds.Members.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Members.Pages {
  public class BarcodeGenModel : PageModel {
    private readonly IHtmlLocalizer H;
    private readonly MemberService _memberService;
    private readonly INotifier _notifier;

    [BindProperty]
    public string LegalName { get; set; }
    [BindProperty]
    public string Oib {
      get; set;
    }
    [BindProperty]
    public decimal Amount {
      get; set;
    }
    [BindProperty]
    public string Note {
      get; set;
    }
    [BindProperty]
    public string PersonId {
      get; set;
    }

    [BindProperty]
    public string OriginalId {
      get; set;
    }

    public List<ContentItem> PersonList { get; set; }

    public BarcodeGenModel(MemberService mService,
        IHtmlLocalizer<CreateMemberModel> htmlLocalizer, INotifier notifier) {
      _notifier = notifier;
      H = htmlLocalizer;
      _memberService = mService;
      Amount = 750;
      Note = "Donatorska vecera";
    }

    public IActionResult OnGetAsync() {
      return Redirect(
          "https://clanovi.glaspoduzetnika.hr/ozds-donacijska-vecera");
    }

    public async Task<IActionResult> OnPost() {
      if (PersonId != OriginalId) {
        ContentItem person = await _memberService.GetContentItemById(PersonId);
        LegalName = person.Content.PersonPart.LegalName.ToString();
        Oib = person.Content.PersonPart.Oib.Text;
        OriginalId = PersonId;
      }
      await SetPersonList();

      return Page();
    }

    public async Task SetPersonList() {
      PersonList = new List<ContentItem>();

      ContentItem ci = await _memberService.GetUserMember();
      if (ci != null) {
        PersonList.Add(ci);

        var companies = await _memberService.GetUserCompanies();
        foreach (ContentItem item in companies) {
          PersonList.Add(item);
        }
      }
    }
  }

}
