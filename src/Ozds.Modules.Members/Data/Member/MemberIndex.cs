using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class MemberIndex : MapIndex
{
  public string UserId { get; init; } = default!;
}

public class MemberIndexProvider : IndexProvider<ContentItem>,
                                       IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<MemberIndex>()
      .Map(item => item.AsReal<Member>()
        .When(member => member.User.UserIds.FirstOrDefault()
          .When(userId =>
          new MemberIndex
          {
            UserId = userId
          }))
        // NOTE: this is mandatory for Yessql
        .NonNullable());
}
