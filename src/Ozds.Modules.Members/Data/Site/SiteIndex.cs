using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class SiteIndex : MapIndex
{
  // TODO: OwnerId
  public string ContentItemId { get; init; } = default!;
  public string SourceTermId { get; init; } = default!;
  public string DeviceId { get; init; } = default!;
  public decimal Coefficient { get; init; } = default!;
  public string PhaseTermId { get; init; } = default!;
  public bool Active { get; init; } = default!;
  public bool Primary { get; init; } = default!;
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
        .As<Site>()
        .WhenNonNullable(
          site =>
          new SiteIndex
          {
            ContentItemId = item.ContentItemId,
            DeviceId = site.DeviceId.Text,
            SourceTermId = site.Source.TermContentItemIds[0],
            Coefficient = site.Coefficient.Value ?? 1,
            PhaseTermId = site.Phase.TermContentItemIds[0],
            Active = site.Active.Value,
            Primary = item.ContentType == "PrimarySite"
          })
        // NOTE: YesSql expects null values
        .NonNullable());
}
