using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class LocationIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
}

public class LocationIndexProvider : IndexProvider<ContentItem>,
                                       IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<LocationIndex>()
      .Map(contentItem => contentItem.As<Location>()
        .WhenNonNullable(person =>
          new LocationIndex
          {
            ContentItemId = contentItem.ContentItemId,
          })
        // NOTE: this is okay because YesSql expects null values
        .NonNullable());
}
