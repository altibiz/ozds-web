using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ReceiptItem : ContentPart
{
  public TextField OrdinalNumber { get; set; } = new();
  public TaxonomyField Article { get; set; } = new();
  public NumericField Amount { get; set; } = new();
  public NumericField Price { get; set; } = new();
  public NumericField InTotal { get; set; } = new();
}

public readonly record struct ReceiptItemData
{
  public readonly string TariffItemTermId { get; init; }
  public readonly string Title { get; init; }
  public readonly string Unit { get; init; }
  public readonly decimal Amount { get; init; }
  public readonly decimal Price { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ReceiptItemData Create(
      TariffTagType tag,
      decimal amount,
      decimal price) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.Title.Value.Title,
      Unit = tag.TariffTag.Value.Unit.Text,
      Amount = amount,
      Price = price,
      InTotal = price * amount
    };

  public static ReceiptItemData Create(
      TariffTagType tag,
      ExpenditureItemData item) =>
    Create(tag, item.Amount, item.UnitPrice);
}

public static class ReceiptItemDataTaxonomyExtensions
{
  public static Task<ReceiptItemData> CreateReceiptItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal amount,
      decimal price) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ReceiptItemData
        .Create(tag, amount, price));

  public static Task<ReceiptItemData> CreateReceiptItemData(
      this TaxonomyCacheService taxonomy,
      ExpenditureItemData item) =>
    taxonomy
      .GetTariffItem(item.TariffItemTermId)
      .ThenWhenNonNullable(tag => ReceiptItemData
        .Create(tag, item));
}
