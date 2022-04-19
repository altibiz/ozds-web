using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Lists.Models;
using System.Linq.Expressions;
using YesSql;
using YesSql.Indexes;
using Ozds.Util;

namespace Ozds.Modules.Members;

public static class SessionExtensions
{
  public static Task<ContentItem> GetItemById(
      this ISession session,
      string contentItemId) =>
    session
      .Query<ContentItem, ContentItemIndex>(
          item => item.ContentItemId == contentItemId)
      .FirstOrDefaultAsync();

  public static Task<ContentItem?> GetListParent(
      this ISession session, ContentItem child) =>
    child
      .As<ContainedPart>()
      .When(part => session
        .GetItemById(part.ListContentItemId)
        .Nullable());

  public static Task<ContentItem> FirstOrDefaultAsync<TIndex>(
      this ISession session,
      IContentManager manager,
      Expression<Func<TIndex, bool>> query) where TIndex : class, IIndex =>
    session
      .Query<ContentItem, TIndex>(query)
      .FirstOrDefaultAsync(manager);
}
