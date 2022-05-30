using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class ReceiptIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string SiteContentItemId { get; init; } = default!;
  public string TariffModelTermId { get; init; } = default!;

  public string OperatorName { get; init; } = default!;
  public string OperatorOib { get; init; } = default!;

  public string CenterContentItemId { get; init; } = default!;
  public string? CenterUserId { get; init; } = default!;
  public string CenterOwnerName { get; init; } = default!;
  public string CenterOwnerOib { get; init; } = default!;

  public string ConsumerContentItemId { get; init; } = default!;
  public string? ConsumerUserId { get; init; } = default!;
  public string ConsumerName { get; init; } = default!;
  public string ConsumerOib { get; init; } = default!;
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
        .WhenNonNull(receipt =>
          // NOTE: no way these are not nullable
          new ReceiptIndex
          {
            ContentItemId = item.ContentItemId,
            SiteContentItemId = receipt.Data.Calculation.SiteContentItemId,
            TariffModelTermId = receipt.Data.Calculation.TariffModelTermId,

            OperatorName = receipt.Data.Operator.Name,
            OperatorOib = receipt.Data.Operator.Oib,

            CenterContentItemId = receipt.Data.CenterOwner.ContentItemId,
            CenterUserId = receipt.Data.CenterUserId,
            CenterOwnerName = receipt.Data.CenterOwner.Name,
            CenterOwnerOib = receipt.Data.CenterOwner.Oib,

            ConsumerContentItemId = receipt.Data.Consumer.ContentItemId,
            ConsumerUserId = receipt.Data.ConsumerUserId,
            ConsumerName = receipt.Data.Consumer.Name,
            ConsumerOib = receipt.Data.Consumer.Oib,
          })
        // NOTE: YesSql expects null here
        .NonNull());
}