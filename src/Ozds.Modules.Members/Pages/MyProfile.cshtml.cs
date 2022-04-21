using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement;

namespace Ozds.Modules.Members.Pages;

public class MyProfileModel : PageModel
{
  public async Task OnGetAsync()
  {
    var member = await Members.GetUserMember();
    Shape = await ContentDisplay.BuildEditorAsync(
        member, UpdateModel.ModelUpdater, false);
  }

  public IShape? Shape { get; set; }

  public MyProfileModel(MemberService mService,
      IContentItemDisplayManager contentItemDisplayManager,
      IHtmlLocalizer<CreateMemberModel> htmlLocalizer,
      IUpdateModelAccessor updateModelAccessor)
  {
    ContentDisplay = contentItemDisplayManager;
    UpdateModel = updateModelAccessor;

    H = htmlLocalizer;

    Members = mService;
  }

  private IContentItemDisplayManager ContentDisplay { get; }
  private IHtmlLocalizer H { get; }
  private IUpdateModelAccessor UpdateModel { get; }
  private MemberService Members { get; }
}
