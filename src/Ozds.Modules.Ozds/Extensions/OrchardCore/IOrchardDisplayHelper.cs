using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Razor;
using OrchardCore.Taxonomies.Fields;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public static class IOrchardDisplayHelperExtensions
{
  public static async Task<IHtmlContent> EditorAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      ContentItem content,
      string groupId = "",
      IUpdateModel? updater = null) =>
    await orchardDisplayHelper.HttpContext.RequestServices
      .GetRequiredService<IContentItemDisplayManager>()
      .BuildEditorAsync(content, updater, true, groupId)
      .ThenAwait(async shape => await orchardDisplayHelper.DisplayHelper
        .ShapeExecuteAsync(shape));

  public static async Task<IHtmlContent> EditorAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string contentType,
      string groupId = "",
      IUpdateModel? updater = null) =>
    await orchardDisplayHelper.HttpContext.RequestServices
      .GetRequiredService<IContentManager>()
      .NewAsync(contentType)
      .ThenAwait(async content => await orchardDisplayHelper
        .EditorAsync(content, groupId, updater));

  public static async Task<IHtmlContent> DisplayTaxonomyTermAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      TaxonomyField taxonomy,
      string? displayType = null) =>
    await orchardDisplayHelper
      .GetTaxonomyTermAsync(
        taxonomy.TaxonomyContentItemId,
        taxonomy.TermContentItemIds.First())
      .ThenAwait(async item => await orchardDisplayHelper
        .DisplayAsync(item, displayType ?? "Detail"));

  public static async Task<IHtmlContent> DisplayByIdAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string id,
      string displayType = "",
      string groupId = "",
      IUpdateModel? updateModel = null) =>
    await orchardDisplayHelper
      .GetContentItemByIdAsync(id)
      .ThenAwait(async item => await orchardDisplayHelper
        .DisplayAsync(
          item,
          displayType,
          groupId,
          updateModel));

  public static async Task<IHtmlContent> DisplayByAliasAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string alias,
      string displayType = "",
      string groupId = "",
      IUpdateModel? updateModel = null) =>
    await orchardDisplayHelper
      .GetContentItemByAliasAsync(alias)
      .ThenAwait(async item => await orchardDisplayHelper
        .DisplayAsync(
          item,
          displayType,
          groupId,
          updateModel));

  public static async Task<IHtmlContent> DisplayBySlugAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string slug,
      string displayType = "",
      string groupId = "",
      IUpdateModel? updateModel = null) =>
    await orchardDisplayHelper
      .GetContentItemBySlugAsync(slug)
      .ThenAwait(async item => await orchardDisplayHelper
        .DisplayAsync(
          item,
          displayType,
          groupId,
          updateModel));

  public static async Task<IHtmlContent> DisplayByHandleAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string handle,
      string displayType = "",
      string groupId = "",
      IUpdateModel? updateModel = null) =>
    await orchardDisplayHelper
      .GetContentItemByHandleAsync(handle)
      .ThenAwait(async item => await orchardDisplayHelper
        .DisplayAsync(
          item,
          displayType,
          groupId,
          updateModel));

  public static async Task<IHtmlContent> DisplayByVersionIdAsync(
      this IOrchardDisplayHelper orchardDisplayHelper,
      string versionId,
      string displayType = "",
      string groupId = "",
      IUpdateModel? updateModel = null) =>
    await orchardDisplayHelper
      .GetContentItemByHandleAsync(versionId)
      .ThenAwait(async item => await orchardDisplayHelper
        .DisplayAsync(
          item,
          displayType,
          groupId,
          updateModel));
}