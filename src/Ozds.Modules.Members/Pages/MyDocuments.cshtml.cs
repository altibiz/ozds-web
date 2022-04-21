using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;

namespace Ozds.Modules.Members.Pages;

public class MyDocumentsModel : PageModel
{
  public async Task OnGetAsync()
  {
    var member = await Members.GetUserMember();
    if (member is null)
    {
      return;
    }

    AddDocument(
        H["Membership"].Value, member.DisplayText, member.ContentItemId);
  }

  public Dictionary<string, List<(string name, string id)>> Documents
  {
    get;
  } = new();

  public MyDocumentsModel(MemberService members,
      IHtmlLocalizer<MyDocumentsModel> localizer, INotifier notifier)
  {
    Members = members;
    H = localizer;
    Notifier = notifier;
  }

  private IHtmlLocalizer H { get; }
  private MemberService Members { get; }
  private INotifier Notifier { get; }

  private void AddDocument(string group, string name, string id)
  {
    if (!Documents.TryGetValue(group, out var links))
    {
      Documents[group] = links = new List<(string, string)>();
    }

    links.Add((name, id));
  }
}