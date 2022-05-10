using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Newtonsoft.Json;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Calculation : ContentPart
{
  public ContentPickerField Site { get; set; } = new();
  public TaxonomyField TariffModel { get; set; } = new();
  public DateField DateFrom { get; set; } = new();
  public DateField DateTo { get; set; } = new();
  public NumericField MeasurementServiceFee { get; set; } = new();

  [JsonIgnore]
  public Lazy<CalculationData> Data { get; }

  public Calculation()
  {
    Data = new Lazy<CalculationData>(
      () =>
        new CalculationData
        {
          SiteContentItemId = Site.ContentItemIds.First(),
          TariffModelTermId = TariffModel.TermContentItemIds.First(),
          DateFrom = DateFrom.Value ?? new(),
          DateTo = DateTo.Value ?? new(),
          UsageExpenditure = ContentItem
            .Get<Expenditure>("UsageExpenditure")!.Data.Value,
          SupplyExpenditure = ContentItem
            .Get<Expenditure>("SupplyExpenditure")!.Data.Value,
        });
  }
}

public readonly record struct CalculationData
{
  public readonly string SiteContentItemId { get; init; }
  public readonly string TariffModelTermId { get; init; }
  public readonly DateTime DateFrom { get; init; }
  public readonly DateTime DateTo { get; init; }
  public readonly ExpenditureData UsageExpenditure { get; init; }
  public readonly ExpenditureData SupplyExpenditure { get; init; }

  public static CalculationData Create(
      DateTime date,
      string siteContentItemId,
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
    new()
    {
      SiteContentItemId = siteContentItemId,
      DateFrom = new DateTime(date.Year, date.AddMonths(-1).Month, 1),
      DateTo = new DateTime(date.Year, date.Month, 1),
      TariffModelTermId = catalogue.TariffModelTermId,
      UsageExpenditure =
        ExpenditureData.FromItems(
          catalogue.Items.SelectFilter(
            item =>
              item.TariffElementTermId switch
              {
                TariffElement.EnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.UsageTermId,
                    ValueFrom = beginEnergy,
                    ValueTo = endEnergy,
                    Amount =
                      endEnergy -
                      beginEnergy,
                    UnitPrice = item.Price,
                    InTotal = item.Price *
                      (endEnergy -
                      beginEnergy),
                  },
                TariffElement.HighCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.HighCostUsageTermId,
                    ValueFrom = beginHighCostEnergy,
                    ValueTo = endHighCostEnergy,
                    Amount =
                      endHighCostEnergy -
                      beginHighCostEnergy,
                    UnitPrice = item.Price,
                    InTotal = item.Price *
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                  },
                TariffElement.LowCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.LowCostUsageTermId,
                    ValueFrom = beginLowCostEnergy,
                    ValueTo = endLowCostEnergy,
                    Amount =
                      endLowCostEnergy -
                      beginLowCostEnergy,
                    UnitPrice = item.Price,
                    InTotal = item.Price *
                      (endLowCostEnergy -
                      beginLowCostEnergy),
                  },
                TariffElement.MaxPowerTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.MaxPowerTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Amount = maxPower,
                    UnitPrice = item.Price,
                    InTotal = item.Price * maxPower
                  },
                TariffElement.SiteFeeTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.MeasurementServiceFeeTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Amount = 1M,
                    UnitPrice = item.Price,
                    InTotal = item.Price
                  },
                _ => null as ExpenditureItemData?,
              }
            )),
      SupplyExpenditure =
        ExpenditureData.FromItems(
          operatorCatalogue.Items.SelectFilter(
            item =>
              item.TariffElementTermId switch
              {
                TariffElement.EnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.SupplyTermId,
                    ValueFrom = beginEnergy,
                    ValueTo = endEnergy,
                    Amount =
                      endEnergy -
                      beginEnergy,
                    UnitPrice = item.Price,
                    InTotal = item.Price *
                      (endEnergy -
                      beginEnergy),
                  },
                TariffElement.HighCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.HighCostSupplyTermId,
                    ValueFrom = beginHighCostEnergy,
                    ValueTo = endHighCostEnergy,
                    Amount =
                      endHighCostEnergy -
                      beginHighCostEnergy,
                    UnitPrice = item.Price,
                    InTotal = item.Price *
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                  },
                TariffElement.LowCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.LowCostSupplyTermId,
                    ValueFrom = beginLowCostEnergy,
                    ValueTo = endLowCostEnergy,
                    Amount =
                      endLowCostEnergy -
                      beginLowCostEnergy,
                    UnitPrice = item.Price,
                    InTotal = item.Price *
                      (endLowCostEnergy -
                      beginLowCostEnergy),
                  },
                _ => null as ExpenditureItemData?
              })
          .Append(ExpenditureItemData
            .CreateRenewableEnergyFee(
              (endEnergy - beginEnergy) +
              (endLowCostEnergy - beginLowCostEnergy) +
              (endHighCostEnergy - beginHighCostEnergy),
              renewableEnergyFeePrice))
          .Append(ExpenditureItemData
            .CreateBusinessUsageFee(
              (endEnergy - beginEnergy) +
              (endLowCostEnergy - beginLowCostEnergy) +
              (endHighCostEnergy - beginHighCostEnergy),
              businessUsageFeePrice))),
    };
}
