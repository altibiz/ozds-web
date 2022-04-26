using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ReceiptIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string OfficialContentItemId { get; init; } = default!;
  public string ContractContentItemId { get; init; } = default!;
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
        .WhenNonNullable(receipt => receipt.Official.ContentItemIds
          .FirstOrDefault()
          .WhenNonNullable(officialId => receipt.Contract.ContentItemIds
            .FirstOrDefault()
            .WhenNonNullable(contractId =>
              new ReceiptIndex
              {
                ContentItemId = item.ContentItemId,
                OfficialContentItemId = officialId,
                ContractContentItemId = contractId,
              })))
        // NOTE: this is okay because YesSql expects null values
        .NonNullable());
}
