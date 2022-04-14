using Ozds.Modules.Members.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YesSql;

namespace Ozds.Modules.Members.Pages;

public class TransparencyModel : PageModel
{
  public TransparencyModel(ISession session, MemberService mService)
  {
    Session = session;
  }

  private ISession Session { get; }
}
