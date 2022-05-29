using Microsoft.AspNetCore.Http;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Lists.Models;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds.Base;

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
      .WhenFinallyTask(
        _ => AdminAttribute.IsApplied(HttpContext),
        item => item
          .As<ContainedPart>()
          .WhenNonNullable(part =>
            Initialize<ContainedPartViewModel>(
              "ContainedPart_Navigation",
              async model => await Content
                .GetAsync(part.ListContentItemId)
                .ThenWith(
                  list =>
                  {
                    model.ListContentItemId = part.ListContentItemId;
                    model.ParentName = list.DisplayText;
                  }))
            .Location("Content"))
          .As<IDisplayResult>()
          // NOTE: it has to be a Task for the override
          .ToTask());

  public ContainedPartDisplayDriver(
      IHttpContextAccessor httpContextAccessor,
      IContentManager content)
  {
    HttpContextAccessor = httpContextAccessor;
    Content = content;
  }

  private IHttpContextAccessor HttpContextAccessor { get; }
  private HttpContext? HttpContext { get => HttpContextAccessor.HttpContext; }

  private IContentManager Content { get; }
}