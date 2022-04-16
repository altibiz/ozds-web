using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Lists.Models;
using System.Linq.Expressions;
using YesSql;
using YesSql.Indexes;

namespace Ozds.Modules.Members;

public static class SessionExtensions
{
  public async static Task<ContentItem> GetItemById(this ISession session,
      string contentItemId) => await session
                                   .Query<ContentItem, ContentItemIndex>(
                                       x => x.ContentItemId == contentItemId)
                                   .FirstOrDefaultAsync();

  public async static Task<ContentItem?> GetListParent(
      this ISession session, ContentItem child)
  {
    var listId = child.As<ContainedPart>()?.ListContentItemId;
    if (listId is null)
    {
      throw new InvalidOperationException($"{child} must be part of a list");
    }

    return await session.GetItemById(listId);
  }

  public async static Task<ContentItem> FirstOrDefaultAsync<TIndex>(
      this ISession session, IContentManager manager,
      Expression<Func<TIndex, bool>> query)
      where TIndex : class,
                     IIndex => await session.Query<ContentItem
                     , TIndex>(query).FirstOrDefaultAsync(manager);
}
