using Microsoft.AspNetCore.Http;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Lists.Models;

namespace Ozds.Modules.Members.Base;

public class ContainedPartViewModel
{
  public string? ListContentItemId { get; set; }
  public string? ParentName { get; set; }
};

public class ContainedPartDisplayDriver : ContentDisplayDriver
{
  public override async Task<IDisplayResult?> EditAsync(
      ContentItem model, BuildEditorContext context)
  {
    if (!AdminAttribute.IsApplied(HttpContext.HttpContext))
    {
      return null;
    }

    var part = model.As<ContainedPart>();
    if (part is null)
    {
      return null;
    }

    MemberContentItem = await Content.GetAsync(part.ListContentItemId);

    return Initialize<ContainedPartViewModel>("ContainedPart_Nav", model =>
    {
      model.ListContentItemId = part.ListContentItemId;
      model.ParentName = MemberContentItem.DisplayText;
    }).Location("Content");
  }

  public ContentItem? MemberContentItem { get; private set; }

  public ContainedPartDisplayDriver(
      IHttpContextAccessor httpContext, IContentManager content)
  {
    HttpContext = httpContext;
    Content = content;
  }

  private IHttpContextAccessor HttpContext { get; }

  private IContentManager Content { get; }
}
