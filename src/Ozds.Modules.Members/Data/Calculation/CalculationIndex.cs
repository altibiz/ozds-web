using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class CalculationIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string OfficialContentItemId { get; init; } = default!;
  public string SiteContentItemId { get; init; } = default!;
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
        .WhenNonNullable(receipt => receipt.Official.ContentItemIds
          .FirstOrDefault()
          .WhenNonNullable(officialId => item.FromBag<Calculation>()
            .WhenNonNullable(calculations => calculations
              .SelectFilter(calculation => calculation.Site.ContentItemIds
                .FirstOrDefault()
                .WhenNonNullable(siteId => calculation.DeviceId.Text
                  .WhenNonNullable(deviceId =>
                    new CalculationIndex
                    {
                      ContentItemId = item.ContentItemId,
                      OfficialContentItemId = officialId,
                      SiteContentItemId = siteId,
                      DeviceId = deviceId,
                    }))))),
          // NOTE: YesSql expects at least an empty enumerable here
          Enumerable.Empty<CalculationIndex>()));
}
