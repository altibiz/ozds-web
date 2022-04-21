using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class CalculationIndex : MapIndex
{
  public string ReceiptId { get; init; } = default!;
  public string OfficialId { get; init; } = default!;
  public string SiteId { get; init; } = default!;
  public string DeviceId { get; init; } = default!;
}

public class CalculationIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<CalculationIndex>()
      .Map(item => item.AsReal<Receipt>()
        .When(receipt => receipt.Official.ContentItemIds
          .FirstOrDefault()
          .When(officialId => item.FromBag<Calculation>()
            .When(calculations => calculations
              .SelectFilter(calculation => calculation.Site.ContentItemIds
                .FirstOrDefault()
                .When(siteId =>
                  new CalculationIndex
                  {
                    ReceiptId = item.ContentItemId,
                    OfficialId = officialId,
                    SiteId = siteId,
                    DeviceId = calculation.DeviceId.Text
                  }))))));

  public CalculationIndexProvider(
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
