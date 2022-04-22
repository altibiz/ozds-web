using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ReceiptIndex : MapIndex
{
  public string OfficialId { get; init; } = default!;
  // NOTE: conflicts with YesSql DocumentId column
  public string ReceiptDocumentId { get; init; } = default!;
  public string Partner { get; init; } = default!;
}

public class ReceiptIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<ReceiptIndex>()
      .Map(contentItem => contentItem.AsReal<Receipt>()
        .When(receipt => receipt.Official.ContentItemIds.FirstOrDefault()
          .When(officialId =>
            new ReceiptIndex
            {
              ReceiptDocumentId = receipt.DocumentId.Text,
              OfficialId = officialId,
              Partner = receipt.Partner.Text,
            }))
        // NOTE: this is mandatory for Yessql
        .NonNullable());
}
