using Microsoft.AspNetCore.Mvc.RazorPages;
using YesSql;

namespace Ozds.Modules.Members.Pages;

public class TransparencyModel : PageModel
{
  public TransparencyModel(ISession session, ConsumerService mService)
  {
    Session = session;
  }

  private ISession Session { get; }
}
