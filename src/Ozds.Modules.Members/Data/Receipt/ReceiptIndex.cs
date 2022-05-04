using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ReceiptIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string SiteContentItemId { get; init; } = default!;
  public string TariffModelTermId { get; init; } = default!;
  public string ConsumerName { get; init; } = default!;
  public string ConsumerOib { get; init; } = default!;
  public string CenterOwnerName { get; init; } = default!;
  public string CenterOwnerOib { get; init; } = default!;
}

public class ReceiptIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<ReceiptIndex>()
      .Map(item => item.AsReal<Receipt>()
        .WhenNonNullable(receipt =>
          // NOTE: no way these are not nullable
          (item.FromBag<Calculation>()?.FirstOrDefault(),
           item.Get<Person>("Consumer").Nullable(),
           item.Get<Person>("Center").Nullable())
          switch
          {
            (Calculation calculation,
             Person consumer,
             Person center) =>
              (calculation.Site.ContentItemIds.FirstOrDefault(),
               calculation.TariffModel.TermContentItemIds.FirstOrDefault())
              switch
              {
                (string siteContentItemId, string tariffModelTermId) =>
                  new ReceiptIndex
                  {
                    ContentItemId = item.ContentItemId,
                    SiteContentItemId =
                      siteContentItemId,
                    TariffModelTermId =
                      tariffModelTermId,
                    ConsumerName = consumer.Name.Text,
                    ConsumerOib = consumer.Oib.Text,
                    CenterOwnerName = center.Name.Text,
                    CenterOwnerOib = center.Oib.Text
                  },
                _ => null
              },
            _ => null
          })
        // NOTE: YesSql expects null here
        .NonNullable());
}
