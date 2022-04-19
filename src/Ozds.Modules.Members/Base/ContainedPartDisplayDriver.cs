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
  public override Task<IDisplayResult?> EditAsync(
      ContentItem model, BuildEditorContext context) =>
    model
      .When(
        _ => AdminAttribute.IsApplied(HttpContext.HttpContext),
        model => ContentItemExtensions
          .As<ContainedPart>(model)
          .When(part => Initialize<ContainedPartViewModel>(
            "ContainedPart_Nav",
            model => Content
              .GetAsync(part.ListContentItemId)
              .Then(list =>
              {
                model.ListContentItemId = part.ListContentItemId;
                model.ParentName = list.DisplayText;
              })
              // NOTE: it needs to be a ValueTask for the Initialize method
              .ToValueTask())
            .Location("Content"))
          .As<IDisplayResult>()
          // NOTE: it has to be a task for the override
          .ToTask());

  public ContainedPartDisplayDriver(
      IHttpContextAccessor httpContext,
      IContentManager content)
  {
    HttpContext = httpContext;
    Content = content;
  }

  private IHttpContextAccessor HttpContext { get; }

  private IContentManager Content { get; }
}
