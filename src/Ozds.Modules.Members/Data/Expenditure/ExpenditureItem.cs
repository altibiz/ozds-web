using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

public class ExpenditureItem : ContentPart
{
  public TaxonomyField TariffItem { get; set; } = new();
  public NumericField ValueFrom { get; set; } = new();
  public NumericField ValueTo { get; set; } = new();
  public NumericField Consumption { get; set; } = new();
  public NumericField UnitPrice { get; set; } = new();
  public NumericField Amount { get; set; } = new();

  [JsonIgnore]
  public Lazy<ExpenditureItemData> Data { get; }

  public ExpenditureItem()
  {
    Data = new Lazy<ExpenditureItemData>(
      () =>
        new ExpenditureItemData
        {
          TariffItemTermId = this.TariffItem.TermContentItemIds.First(),
          ValueFrom = this.ValueFrom.Value ?? 0,
          ValueTo = this.ValueTo.Value ?? 0,
          Amount = this.Consumption.Value ?? 0,
          UnitPrice = this.UnitPrice.Value ?? 0,
          InTotal = this.Amount.Value ?? 0,
        });
  }
}

public readonly record struct ExpenditureItemData
{
  public readonly string TariffItemTermId { get; init; }
  public readonly decimal ValueFrom { get; init; }
  public readonly decimal ValueTo { get; init; }
  public readonly decimal Amount { get; init; }
  public readonly decimal UnitPrice { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ExpenditureItemData CreateRenewableEnergyFee(
      decimal amount,
      decimal price) =>
    new()
    {
      TariffItemTermId = TariffItem.RenewableEnergyFeeTermId,
      ValueFrom = default,
      ValueTo = default,
      Amount = amount,
      UnitPrice = price,
      InTotal = price * amount
    };

  public static ExpenditureItemData CreateBusinessUsageFee(
      decimal amount,
      decimal price) =>
    new()
    {
      TariffItemTermId = TariffItem.BusinessUsageFeeTermId,
      ValueFrom = default,
      ValueTo = default,
      Amount = amount,
      UnitPrice = price,
      InTotal = price * amount
    };
}
