using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class SiteIndex : MapIndex
{
  // TODO: OwnerId
  public string SiteId { get; init; } = default!;
  public string Source { get; init; } = default!;
  public string DeviceId { get; init; } = default!;
  public decimal Coefficient { get; init; } = default!;
  public string Phase { get; init; } = default!;
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
        .WhenNonNullableTask(site => TaxonomyCache
          .GetTerm<TitlePart>(site.Phase)
          .ThenWhenTask(phase => TaxonomyCache
            .GetTerm<TitlePart>(site.Source)
            .ThenFinally(source =>
              new SiteIndex
              {
                SiteId = item.ContentItemId,
                DeviceId = site.DeviceId.Text,
                Source = source.Title,
                Coefficient = site.Coefficient.Value ?? 1,
                Phase = phase.Title,
                Active = site.Active.Value,
                Primary = item.ContentType == "PrimarySite"
              })))
          // NOTE: YesSql expects null values
          .NonNullable());

  public SiteIndexProvider(
      TaxonomyCacheService taxonomyCache)
  {
    TaxonomyCache = taxonomyCache;
  }

  private TaxonomyCacheService TaxonomyCache { get; }
}
