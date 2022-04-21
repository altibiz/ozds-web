using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Title.Models;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class SiteIndex : MapIndex
{
  public string OwnerId { get; init; } = default!;
  public string DeviceId { get; init; } = default!;
  public string Type { get; init; } = default!;
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
      .Map(item => item.FromBag<Site>()
        .When(sites => sites
          .SelectFilter(site => TaxonomyCache.GetTerm<TitlePart>(site.Type)
            .ThenWhen(title => TaxonomyCache.GetTerm<TitlePart>(site.Phase)
              .ThenFinally(phase =>
                new SiteIndex
                {
                  OwnerId = item.ContentItemId,
                  DeviceId = site.DeviceId.Text,
                  Type = title.Title,
                  Coefficient = site.Coefficient.Value ?? 1,
                  Phase = phase.Title,
                  Active = site.Active.Value,
                  Primary = site.Primary
                }))))
        ?.Await());

  public SiteIndexProvider(
      IServiceProvider services,
      IContentDefinitionManager content,
      TaxonomyCacheService taxonomyCache)
  {
    Services = services;
    TaxonomyCache = taxonomyCache;
  }

  private IServiceProvider Services { get; }
  private TaxonomyCacheService TaxonomyCache { get; }
}
