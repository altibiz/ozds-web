using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class MemberIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string UserId { get; init; } = default!;
}

public class MemberIndexProvider : IndexProvider<ContentItem>,
                                       IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<MemberIndex>()
      .Map(item => item
        .AsReal<Member>()
        .WhenNonNullable(member => member.User.UserIds
          .FirstOrDefault()
          .WhenNonNullable(userId =>
            new MemberIndex
            {
              ContentItemId = item.ContentItemId,
              UserId = userId
            }))
        // NOTE: this is okay because YesSql expects null values
        .NonNullable());
}
