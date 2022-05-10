using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;

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
  public readonly decimal Amount { get; init; }
  public readonly decimal Price { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ReceiptItemData FromUsageExpenditureItem(
      ExpenditureItemData item) =>
    FromExpenditureItem(item);

  public static ReceiptItemData FromSupplyExpenditureItem(
      ExpenditureItemData item) =>
    FromExpenditureItem(item);

  public static ReceiptItemData CreateRenewableEnergyFee(
      decimal amount,
      decimal price) =>
    new ReceiptItemData
    {
      TariffItemTermId = TariffItem.RenewableEnergyFeeTermId,
      Amount = amount,
      Price = price,
      InTotal = price * amount
    };

  public static ReceiptItemData CreateBusinessUsageFee(
      decimal amount,
      decimal price) =>
    new ReceiptItemData
    {
      TariffItemTermId = TariffItem.BusinessUsageFeeTermId,
      Amount = amount,
      Price = price,
      InTotal = price * amount
    };

  private static ReceiptItemData FromExpenditureItem(
      ExpenditureItemData item) =>
    new ReceiptItemData
    {
      TariffItemTermId = item.TariffItemTermId,
      Amount = item.Amount,
      Price = item.UnitPrice,
      InTotal = item.InTotal
    };
}
