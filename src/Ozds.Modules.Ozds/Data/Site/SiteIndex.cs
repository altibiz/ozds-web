using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Ozds;

public class SiteIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
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
        ((item.AsReal<SecondarySite>(),
          item.As<Site>().Nullable()) switch
        {
          (SecondarySite secondary, Site site) =>
          (site.Source.TermContentItemIds.FirstOrDefault(),
           site.Status.TermContentItemIds.FirstOrDefault())
          switch
          {
            (string sourceTermId,
             string statusTermId) =>
              new SiteIndex
              {
                ContentItemId = item.ContentItemId,
                SourceTermId = sourceTermId,
                DeviceId = site.DeviceId.Text,
                StatusTermId = statusTermId,
              },
            _ => null
          },
          _ => null,
        })
        // NOTE: YesSql expects null values here
        .NonNullable());
}
