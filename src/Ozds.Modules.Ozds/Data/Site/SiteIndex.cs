using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Ozds;

public class SiteIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string OwnerContentItemId { get; init; } = default!;
  public string UserContentItemId { get; init; } = default!;
  public string SourceTermId { get; init; } = default!;
  public string DeviceId { get; init; } = default!;
  public string StatusTermId { get; init; } = default!;
}

public class SiteIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<SiteIndex>()
      .Map(item =>
        ((item.AsContent<SecondarySiteType>()) switch
        {
          (SecondarySiteType site) =>
          new SiteIndex
          {
            ContentItemId = item.ContentItemId,
            OwnerContentItemId = site.ContainedPart.Value
              .ThrowWhenNull().ListContentItemId,
            SourceTermId = site.Site.Value.Source.TermContentItemIds
              .First(),
            DeviceId = site.Site.Value.DeviceId.Text,
            StatusTermId = site.Site.Value.Status.TermContentItemIds
              .First(),
          },
          _ => null,
        })
        // NOTE: YesSql expects null values here
        .NonNullable());
}
