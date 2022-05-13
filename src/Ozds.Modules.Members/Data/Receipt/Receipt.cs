using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ReceiptType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<Receipt> Receipt { get; init; } = default!;

  private ReceiptType(ContentItem item) : base(item) { }
}

public class Receipt : ContentPart
{
  public ContentPickerField Site { get; set; } = new();
  public DateField Date { get; set; } = new();
  public DateField DateFrom { get; set; } = new();
  public DateField DateTo { get; set; } = new();
  public ReceiptData Data { get; set; } = default;
}

public readonly record struct ReceiptData
{
  public readonly PersonData Operator { get; init; }
  public readonly PersonData CenterOwner { get; init; }
  public readonly string CenterTitle { get; init; }
  public readonly PersonData Consumer { get; init; }
  public readonly CalculationData Calculation { get; init; }
  public readonly ReceiptItemData[] Items { get; init; }
  public readonly decimal UsageFee { get; init; }
  public readonly decimal SupplyFee { get; init; }
  public readonly decimal InTotal { get; init; }
  public readonly decimal Tax { get; init; }
  public readonly decimal InTotalWithTax { get; init; }

  public static Task<ReceiptData> Create(
      TaxonomyCacheService taxonomy,
      PersonData @operator,
      PersonData centerOwner,
      string centerTitle,
      PersonData consumer,
      CalculationData calculation,
      decimal taxRate) =>
    Enumerable.Concat(
      calculation.UsageExpenditure.Items.Select(
        item => taxonomy.CreateReceiptItemData(item)),
      calculation.SupplyExpenditure.Items.Select(
        item => taxonomy.CreateReceiptItemData(item)))
    .Await()
    .Then(items =>
      items.ToArray())
    .Then(items => (items, inTotal: items.Sum(item => item.InTotal)))
    .Then(cached =>
      new ReceiptData
      {
        Operator = @operator,
        CenterOwner = centerOwner,
        CenterTitle = centerTitle,
        Consumer = consumer,
        Calculation = calculation,
        Items = cached.items,
        UsageFee = cached.items
          .Where(item => TariffItem.IsUsage(item.TariffItemTermId))
          .Sum(item => item.InTotal),
        SupplyFee = cached.items
          .Where(item => TariffItem.IsSupply(item.TariffItemTermId))
          .Sum(item => item.InTotal),
        InTotal = cached.inTotal,
        Tax = cached.inTotal * taxRate,
        InTotalWithTax = cached.inTotal + cached.inTotal * taxRate,
      });
}
