using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Razor;
using Ozds.Util;

namespace Ozds.Modules.Members;

public static class IOrchardDisplayHelperExtensions
{
  public static Task<IHtmlContent> EditorAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      ContentItem content,
      string groupId = "",
      IUpdateModel? updater = null) =>
    orchardDisplayHelper.HttpContext.RequestServices
      .GetRequiredService<IContentItemDisplayManager>()
      .BuildEditorAsync(content, updater, true, groupId)
      .ThenTask(shape => orchardDisplayHelper.DisplayHelper
        .ShapeExecuteAsync(shape));

  public static Task<IHtmlContent> EditorAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string contentType,
      string groupId = "",
      IUpdateModel? updater = null) =>
    orchardDisplayHelper.HttpContext.RequestServices
      .GetRequiredService<IContentManager>()
      .NewAsync(contentType)
      .ThenTask(content => orchardDisplayHelper
        .EditorAsync(content, groupId, updater));
}