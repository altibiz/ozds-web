using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
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
      .Map(
        item =>
        {
          var receipt = item.AsReal<Receipt>();
          if (receipt is null)
          {
            return Enumerable.Empty<CalculationIndex>();
          }

          var officialId = receipt.Official.ContentItemIds.FirstOrDefault();
          if (officialId is null)
          {
            return Enumerable.Empty<CalculationIndex>();
          }

          var calculations = item.FromBag<Calculation>();
          if (calculations is null)
          {
            return Enumerable.Empty<CalculationIndex>();
          }

          var result = calculations.SelectFilter(
            calculation =>
            {
              var siteId = calculation.Site.ContentItemIds.FirstOrDefault();
              if (siteId is null)
              {
                return null;
              }

              var deviceId = calculation.DeviceId.Text;
              if (deviceId is null)
              {
                return null;
              }

              return new CalculationIndex
              {
                ReceiptId = item.ContentItemId,
                OfficialId = officialId,
                SiteId = siteId,
                DeviceId = deviceId,
              };
            });

          return result;
        });
}
