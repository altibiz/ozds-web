using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;
using Ozds.Util;

namespace Ozds.Modules.Members.Pages
{
  [Authorize]
  public class CreateMemberModel : PageModel
  {
    public Task<IActionResult> OnGetAsync() =>
      Members
        .GetUserMember(includeDraft: true)
        .ThenValueTask(member => member
          .WhenNonNullable(
            _ => RedirectToPage("Index") as IActionResult,
            () => Members
              .GetNewItem(ContentType.Member)
              .AfterTask(async () =>
              {
                (_, Shape) = await Members.GetNewItem(ContentType.Member);
                return Page() as IActionResult;
              })));

    public Task<IActionResult> OnPostCreateMemberAsync() =>
      CreatePost("Index");

    private Task<IActionResult> CreatePost(string nextPage) =>
      Members
        .ModelToNew(ContentType.Member)
        .ThenTask(
          @new =>
          {
            (_, Shape) = @new;
            return !ModelState.IsValid ? (Page() as IActionResult).ToTask()
              : Members.CreateMemberDraft(@new.item)
                  .ThenValueTask(result => result
                    .WhenFinallyValueTask(
                      result => result.Succeeded,
                      _ => Notifier
                        .SuccessAsync(H["Member registration successful"])
                        .After(() => RedirectToPage(nextPage) as IActionResult),
                      () => Page() as IActionResult));
          });

    public IShape? Shape { get; private set; }

    public CreateMemberModel(
        IHtmlLocalizer<CreateMemberModel> localizer,
        ConsumerService members,
        INotifier notifier)
    {
      H = localizer;

      Members = members;
      Notifier = notifier;
    }

    private readonly IHtmlLocalizer H;
    private readonly ConsumerService Members;
    private readonly INotifier Notifier;
  }
}
