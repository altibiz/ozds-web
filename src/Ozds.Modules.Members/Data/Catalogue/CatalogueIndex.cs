using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class CatalogueIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
}

public class CatalogueIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<CatalogueIndex>()
      .Map(item => item.AsReal<Catalogue>()
        .WhenNonNullable(
          catalogue =>
            new CatalogueIndex
            {
              ContentItemId = item.ContentItemId
            })
        // NOTE: YesSql expects a null value
        .NonNullable());
}
