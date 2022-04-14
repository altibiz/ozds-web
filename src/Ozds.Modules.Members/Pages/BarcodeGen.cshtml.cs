using Ozds.Modules.Members.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Modules.Members.Pages;

public class BarcodeGenModel : PageModel
{
  public IActionResult OnGetAsync()
  {
    return Redirect(
        "https://clanovi.glaspoduzetnika.hr/ozds-donacijska-vecera");
  }

  public async Task<IActionResult> OnPost()
  {
    if (PersonId != OriginalId && PersonId is not null)
    {
      ContentItem person = await Members.GetContentItemById(PersonId);
      LegalName = person.Content.PersonPart.LegalName.ToString();
      Oib = person.Content.PersonPart.Oib.Text;
      OriginalId = PersonId;
    }

    PersonList.Clear();
    ContentItem? item = await Members.GetUserMember();
    if (item is not null)
    {
      PersonList.Add(item);
    }

    return Page();
  }

  [BindProperty]
  public string? LegalName { get; set; }

  [BindProperty]
  public string? Oib
  {
    get; set;
  }

  [BindProperty]
  public decimal? Amount
  {
    get; set;
  }

  [BindProperty]
  public string? Note
  {
    get; set;
  }

  [BindProperty]
  public string? PersonId
  {
    get; set;
  }

  [BindProperty]
  public string? OriginalId
  {
    get; set;
  }

  public List<ContentItem> PersonList { get; } = new();

  public BarcodeGenModel(MemberService mService,
      IHtmlLocalizer<CreateMemberModel> localizer, INotifier notifier)
  {
    Notifier = notifier;
    H = localizer;
    Members = mService;
    Amount = 750;
    Note = "Donatorska vecera";
  }

  private IHtmlLocalizer H { get; }
  private MemberService Members { get; }
  private INotifier Notifier { get; }
}
