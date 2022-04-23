using YesSql.Indexes;
using OrchardCore.ContentManagement;
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
        .WhenNonNullable(center => center.User.UserIds.FirstOrDefault()
          .WhenNonNullable(userId =>
            new CenterIndex
            {
              UserId = userId
            }))
        // NOTE: this is mandatory for Yessql
        .NonNullable());
}
