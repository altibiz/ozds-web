using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;

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
  public ReceiptData Data { get; set; } = default;
}

public readonly record struct ReceiptData
{
  public readonly PersonData Operator { get; init; }
  public readonly PersonData CenterOwner { get; init; }
  public readonly PersonData Consumer { get; init; }
  public readonly CalculationData Calculation { get; init; }
  public readonly IEnumerable<ReceiptItemData> Items { get; init; }
  public readonly decimal InTotal { get; init; }
  public readonly decimal Tax { get; init; }
  public readonly decimal InTotalWithTax { get; init; }

  public static ReceiptData FromCalculation(
      PersonData @operator,
      PersonData centerOwner,
      PersonData consumer,
      CalculationData calculation,
      decimal taxRate)
  {
    var items =
      Enumerable.Concat(
        calculation.UsageExpenditure.Items.Select(
          ReceiptItemData.FromUsageExpenditureItem),
        calculation.SupplyExpenditure.Items.Select(
          ReceiptItemData.FromSupplyExpenditureItem));

    var inTotal = items.Sum(item => item.InTotal);
    var tax = taxRate * inTotal;
    var inTotalWithTax = inTotal + tax;

    return
      new ReceiptData
      {
        Operator = @operator,
        CenterOwner = centerOwner,
        Consumer = consumer,
        Calculation = calculation,
        Items = items,
        InTotal = inTotal,
        Tax = tax,
        InTotalWithTax = inTotalWithTax,
      };
  }
}
