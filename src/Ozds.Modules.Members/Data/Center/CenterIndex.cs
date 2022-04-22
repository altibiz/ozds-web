using YesSql.Indexes;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class CenterIndex : MapIndex
{
  public string UserId { get; init; } = default!;
}

public class CenterIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<CenterIndex>()
      .Map(item => item.AsReal<Center>()
        .When(center => center.User.UserIds.FirstOrDefault()
          .When(userId =>
          new CenterIndex
          {
            UserId = userId
          }))
        // NOTE: this is mandatory for Yessql
        .NonNullable());
}
