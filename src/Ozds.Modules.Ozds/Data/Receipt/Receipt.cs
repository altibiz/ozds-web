using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class ReceiptType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<Receipt> Receipt { get; init; } = default!;

  private ReceiptType(ContentItem item) : base(item) { }
}

public class Receipt : ContentPart
{
  public ContentPickerField Site { get; init; } = new();
  public DateField Date { get; init; } = new();
  public DateField DateFrom { get; init; } = new();
  public DateField DateTo { get; init; } = new();

  public ReceiptData Data { get; set; } = default;
}

public readonly record struct ReceiptData
{
  public readonly PersonData Operator { get; init; }

  public readonly PersonData CenterOwner { get; init; }
  public readonly string? CenterUserId { get; init; }
  public readonly string CenterTitle { get; init; }

  public readonly PersonData Consumer { get; init; }
  public readonly string? ConsumerUserId { get; init; }

  public readonly CalculationData Calculation { get; init; }
  public readonly ReceiptItemData[] Items { get; init; }
  public readonly decimal UsageFee { get; init; }
  public readonly decimal SupplyFee { get; init; }
  public readonly decimal InTotal { get; init; }
  public readonly decimal Tax { get; init; }
  public readonly decimal InTotalWithTax { get; init; }

  public static Task<ReceiptData> Create(
      TaxonomyCacheService taxonomy,
      string? centerUserId,
      PersonData @operator,
      PersonData centerOwner,
      string centerTitle,
      string? consumerUderId,
      PersonData consumer,
      CalculationData calculation,
      decimal taxRate) =>
    Enumerable.Concat(
      calculation.UsageExpenditure.Items.Select(
        item => taxonomy.CreateReceiptItemData(item)),
      calculation.SupplyExpenditure.Items.Select(
        item => taxonomy.CreateReceiptItemData(item)))
    .Await()
    .Then(items => items.ToArray())
    .Then(items => (items, inTotal: items.Sum(item => item.InTotal)))
    .Then(data =>
      new ReceiptData
      {
        CenterUserId = centerUserId,
        Operator = @operator,
        CenterOwner = centerOwner,
        CenterTitle = centerTitle,
        ConsumerUserId = consumerUderId,
        Consumer = consumer,
        Calculation = calculation,
        Items = data.items,
        UsageFee = decimal.Round(
          data.items
            .Where(item => TariffItem.IsUsage(item.TariffItemTermId))
            .Sum(item => item.InTotal),
          2),
        SupplyFee = decimal.Round(
          data.items
            .Where(item => TariffItem.IsSupply(item.TariffItemTermId))
            .Sum(item => item.InTotal),
          2),
        InTotal = decimal.Round(data.inTotal, 2),
        Tax = decimal.Round(data.inTotal * taxRate, 2),
        InTotalWithTax = decimal.Round(
          data.inTotal + decimal.Round(data.inTotal * taxRate, 2),
          2),
      });
}

public readonly record struct CalculationData
{
  public readonly string SiteContentItemId { get; init; }
  public readonly string SiteTitle { get; init; }
  public readonly string TariffModelTermId { get; init; }
  public readonly string TariffModelTitle { get; init; }
  public readonly DateTime DateFrom { get; init; }
  public readonly DateTime DateTo { get; init; }
  public readonly ExpenditureData UsageExpenditure { get; init; }
  public readonly ExpenditureData SupplyExpenditure { get; init; }

  public static Task<CalculationData> Create(
      TaxonomyCacheService taxonomy,
      DateTime date,
      DateTime beginDate,
      DateTime endDate,
      string siteContentItemId,
      string siteTitle,
      CatalogueData catalogue,
      CatalogueData operatorCatalogue,
      decimal beginEnergy,
      decimal endEnergy,
      decimal beginHighCostEnergy,
      decimal endHighCostEnergy,
      decimal beginLowCostEnergy,
      decimal endLowCostEnergy,
      decimal maxPower,
      decimal renewableEnergyFeePrice,
      decimal businessUsageFeePrice) =>
    (ExpenditureData.CreateUsage(
      taxonomy,
      catalogue,
      beginEnergy,
      endEnergy,
      beginHighCostEnergy,
      endHighCostEnergy,
      beginLowCostEnergy,
      endLowCostEnergy,
      maxPower),
     ExpenditureData.CreateSupply(
      taxonomy,
      operatorCatalogue,
      beginEnergy,
      endEnergy,
      beginHighCostEnergy,
      endHighCostEnergy,
      beginLowCostEnergy,
      endLowCostEnergy,
      renewableEnergyFeePrice,
      businessUsageFeePrice),
     taxonomy.GetTariffModel(catalogue.TariffModelTermId))
    .Await()
    .Then(data => data switch
      {
        (ExpenditureData usage,
         ExpenditureData supply,
         TagType model) =>
          new CalculationData
          {
            SiteContentItemId = siteContentItemId,
            SiteTitle = siteTitle,
            TariffModelTermId = catalogue.TariffModelTermId,
            TariffModelTitle = model.Title.Value.Title,
            DateFrom = beginDate,
            DateTo = endDate,
            UsageExpenditure = data.Item1,
            SupplyExpenditure = data.Item2,
          },
        _ => default
      });
}

public readonly record struct ExpenditureData
{
  public readonly ExpenditureItemData[] Items { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ExpenditureData Create(
      IEnumerable<ExpenditureItemData> items) =>
    new()
    {
      Items = items.ToArray(),
      InTotal = items.Sum(item => item.InTotal),
    };

  public static Task<ExpenditureData> CreateUsage(
      TaxonomyCacheService taxonomy,
      CatalogueData catalogue,
      decimal beginEnergy,
      decimal endEnergy,
      decimal beginHighCostEnergy,
      decimal endHighCostEnergy,
      decimal beginLowCostEnergy,
      decimal endLowCostEnergy,
      decimal maxPower) =>
    catalogue.Items.SelectFilter(
      item =>
        item.TariffElementTermId switch
        {
          TariffElement.EnergyTermId =>
            taxonomy.CreateExpenditureItemData(
              TariffItem.UsageTermId,
              beginEnergy,
              endEnergy,
              item.Price),
          TariffElement.HighCostEnergyTermId =>
            taxonomy.CreateExpenditureItemData(
              TariffItem.HighCostUsageTermId,
              beginHighCostEnergy,
              endHighCostEnergy,
              item.Price),
          TariffElement.LowCostEnergyTermId =>
            taxonomy.CreateExpenditureItemData(
              TariffItem.LowCostUsageTermId,
              beginLowCostEnergy,
              endLowCostEnergy,
              item.Price),
          TariffElement.MaxPowerTermId =>
            taxonomy.CreateExpenditureItemData(
              TariffItem.MaxPowerTermId,
              maxPower,
              item.Price),
          TariffElement.SiteFeeTermId =>
            taxonomy.CreateExpenditureItemData(
              TariffItem.MeasurementServiceFeeTermId,
              item.Price),
          _ => default,
        })
    .Await()
    .Then(items => Create(items));

  public static Task<ExpenditureData> CreateSupply(
      TaxonomyCacheService taxonomy,
      CatalogueData catalogue,
      decimal beginEnergy,
      decimal endEnergy,
      decimal beginHighCostEnergy,
      decimal endHighCostEnergy,
      decimal beginLowCostEnergy,
      decimal endLowCostEnergy,
      decimal renewableEnergyFeePrice,
      decimal businessUsageFeePrice) =>
    catalogue.Items.SelectFilter(item =>
      item.TariffElementTermId switch
      {
        TariffElement.EnergyTermId =>
          taxonomy.CreateExpenditureItemData(
            TariffItem.SupplyTermId,
            beginEnergy,
            endEnergy,
            item.Price),
        TariffElement.HighCostEnergyTermId =>
          taxonomy.CreateExpenditureItemData(
            TariffItem.HighCostSupplyTermId,
            beginHighCostEnergy,
            endHighCostEnergy,
            item.Price),
        TariffElement.LowCostEnergyTermId =>
          taxonomy.CreateExpenditureItemData(
            TariffItem.LowCostSupplyTermId,
            beginLowCostEnergy,
            endLowCostEnergy,
            item.Price),
        _ => default,
      })
      .Await()
      .ThenTask(items =>
        (taxonomy.GetTariffItem(TariffItem.RenewableEnergyFeeTermId),
         taxonomy.GetTariffItem(TariffItem.BusinessUsageFeeTermId))
          .Await()
          .Then(data =>
            data switch
            {
              (TariffTagType renewableEnergyFeeTag,
               TariffTagType businessUsageTag) =>
              items
                .Append(ExpenditureItemData
                  .Create(
                    renewableEnergyFeeTag,
                    items
                      .Where(item =>
                        item.TariffItemTermId.In(
                          TariffItem.SupplyTermId,
                          TariffItem.HighCostSupplyTermId,
                          TariffItem.LowCostSupplyTermId))
                      .Sum(item => item.Amount),
                    renewableEnergyFeePrice))
                .Append(ExpenditureItemData
                  .Create(
                    businessUsageTag,
                    items
                      .Where(item =>
                        item.TariffItemTermId.In(
                          TariffItem.SupplyTermId,
                          TariffItem.HighCostSupplyTermId,
                          TariffItem.LowCostSupplyTermId))
                      .Sum(item => item.Amount),
                    businessUsageFeePrice)),
              _ => items
            }))
      .Then(items => Create(items));
}

public readonly record struct ExpenditureItemData
{
  public readonly string TariffItemTermId { get; init; }
  public readonly string Title { get; init; }
  public readonly decimal ValueFrom { get; init; }
  public readonly decimal ValueTo { get; init; }
  public readonly decimal Amount { get; init; }
  public readonly decimal UnitPrice { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ExpenditureItemData Create(
      TariffTagType tag,
      decimal valueFrom,
      decimal valueTo,
      decimal unitPrice) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.TariffTag.Value.Abbreviation.Text,
      ValueFrom = valueFrom,
      ValueTo = valueTo,
      Amount = decimal.Round(valueTo - valueFrom),
      UnitPrice = unitPrice,
      InTotal = decimal.Round(
        decimal.Round(valueTo - valueFrom, 2) * unitPrice,
        2),
    };

  public static ExpenditureItemData Create(
      TariffTagType tag,
      decimal amount,
      decimal unitPrice) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.TariffTag.Value.Abbreviation.Text,
      ValueFrom = default,
      ValueTo = default,
      Amount = decimal.Round(amount),
      UnitPrice = unitPrice,
      InTotal = decimal.Round(unitPrice * amount, 2)
    };

  public static ExpenditureItemData Create(
      TariffTagType tag,
      decimal unitPrice) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.TariffTag.Value.Abbreviation.Text,
      ValueFrom = default,
      ValueTo = default,
      Amount = 1M,
      UnitPrice = unitPrice,
      InTotal = decimal.Round(unitPrice, 2)
    };
}

public static class ExpenditureItemDataTaxonomyExtensions
{
  public static Task<ExpenditureItemData> CreateExpenditureItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal valueFrom,
      decimal valueTo,
      decimal unitPrice) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ExpenditureItemData
        .Create(tag, valueFrom, valueTo, unitPrice));

  public static Task<ExpenditureItemData> CreateExpenditureItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal amount,
      decimal unitPrice) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ExpenditureItemData
        .Create(tag, amount, unitPrice));

  public static Task<ExpenditureItemData> CreateExpenditureItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal unitPrice) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ExpenditureItemData
        .Create(tag, unitPrice));
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
      Amount = decimal.Round(amount),
      Price = price,
      InTotal = decimal.Round(price * amount, 2)
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
