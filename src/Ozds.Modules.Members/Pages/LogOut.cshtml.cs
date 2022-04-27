using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Users;

namespace Ozds.Modules.Members.Pages;

public class LogOutModel : PageModel
{
  public async Task<IActionResult> OnGetAsync()
  {
    await SignInManager.SignOutAsync();
    await Notifier.SuccessAsync(H["LogOut Success"]);
    return Page();
  }

  public LogOutModel(
      SignInManager<IUser> signInManager,
      INotifier notifier,
      IHtmlLocalizer<LogOutModel> htmlLocalizer)
  {
    SignInManager = signInManager;
    Notifier = notifier;

    H = htmlLocalizer;
  }

  private SignInManager<IUser> SignInManager { get; }
  private INotifier Notifier { get; }
  private IHtmlLocalizer<LogOutModel> H { get; }
}
