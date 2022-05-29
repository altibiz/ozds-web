using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Extensions;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class SiteIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string SourceTermId { get; init; } = default!;
  public string DeviceId { get; init; } = default!;
  public string StatusTermId { get; init; } = default!;

  public string OperatorName { get; init; } = default!;
  public string OperatorOib { get; init; } = default!;

  public string CenterContentItemId { get; init; } = default!;
  public string? CenterUserId { get; init; } = default!;
  public string CenterOwnerName { get; init; } = default!;
  public string CenterOwnerOib { get; init; } = default!;

  public string OwnerContentItemId { get; init; } = default!;
  public string? OwnerUserId { get; init; } = default!;
  public string OwnerName { get; init; } = default!;
  public string OwnerOib { get; init; } = default!;
}

public class SiteIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<SiteIndex>()
      .Map(item => item
        .AsContent<SecondarySiteType>()
        .WhenNonNullable(site =>
          new SiteIndex
          {
            ContentItemId = item.ContentItemId,
            SourceTermId = site.Site.Value.Source.TermContentItemIds.First(),
            DeviceId = Device.MakeId(
                SiteMeasurementSource
                  .GetElasticsearchSource(
                    site.Site.Value.Source.TermContentItemIds.First())
                  .ThrowWhenNull(),
                site.Site.Value.SourceDeviceId.Text),
            StatusTermId = site.Site.Value.Status.TermContentItemIds.First(),

            OperatorName = site.Site.Value.Data.OperatorName,
            OperatorOib = site.Site.Value.Data.OperatorOib,

            CenterOwnerName = site.Site.Value.Data.CenterOwnerName,
            CenterOwnerOib = site.Site.Value.Data.CenterOwnerOib,
            CenterContentItemId = site.Site.Value.Data.CenterContentItemId,
            CenterUserId = site.Site.Value.Data.CenterUserId,

            OwnerName = site.Site.Value.Data.OwnerName,
            OwnerOib = site.Site.Value.Data.OwnerOib,
            OwnerContentItemId = site.Site.Value.Data.OwnerContentItemId,
            OwnerUserId = site.Site.Value.Data.OwnerUserId,
          })
        // NOTE: YesSql expects null values here
        .NonNullable());
}
