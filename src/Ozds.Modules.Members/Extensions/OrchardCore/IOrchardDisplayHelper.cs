using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Razor;
using OrchardCore.Taxonomies.Fields;
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

  public static Task<IHtmlContent> DisplayTaxonomyTermAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      TaxonomyField taxonomy,
      string? displayType = null) =>
    orchardDisplayHelper
      .GetTaxonomyTermAsync(
        taxonomy.TaxonomyContentItemId,
        taxonomy.TermContentItemIds[0])
      .ThenTask(item => orchardDisplayHelper
        .DisplayAsync(item, displayType ?? "Detail"));

  public static Task<IHtmlContent> DisplayByIdAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string id) =>
    orchardDisplayHelper
      .GetContentItemByIdAsync(id)
      .ThenTask(item => orchardDisplayHelper.DisplayAsync(item));

  public static Task<IHtmlContent> DisplayByAliasAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string alias) =>
    orchardDisplayHelper
      .GetContentItemByAliasAsync(alias)
      .ThenTask(item => orchardDisplayHelper.DisplayAsync(item));

  public static Task<IHtmlContent> DisplayBySlugAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string slug) =>
    orchardDisplayHelper
      .GetContentItemBySlugAsync(slug)
      .ThenTask(item => orchardDisplayHelper.DisplayAsync(item));

  public static Task<IHtmlContent> DisplayByHandleAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string handle) =>
    orchardDisplayHelper
      .GetContentItemByHandleAsync(handle)
      .ThenTask(item => orchardDisplayHelper.DisplayAsync(item));

  public static Task<IHtmlContent> DisplayByVersionIdAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string versionId) =>
    orchardDisplayHelper
      .GetContentItemByHandleAsync(versionId)
      .ThenTask(item => orchardDisplayHelper.DisplayAsync(item));
}