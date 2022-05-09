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
          SiteContentItemId = this.Site.ContentItemIds.First(),
          TariffModelTermId = this.TariffModel.TermContentItemIds.First(),
          DateFrom = this.DateFrom.Value ?? new(),
          DateTo = this.DateTo.Value ?? new(),
          MeasurementServiceFee = this.MeasurementServiceFee.Value ?? 0,
          UsageExpenditure = this.ContentItem
            .Get<Expenditure>("UsageExpenditure")!.Data.Value,
          SupplyExpenditure = this.ContentItem
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
  public readonly decimal MeasurementServiceFee { get; init; }
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
      decimal maxPower) =>
    new CalculationData
    {
      SiteContentItemId = siteContentItemId,
      DateFrom = new DateTime(date.Year, date.AddMonths(-1).Month, 1),
      DateTo = new DateTime(date.Year, date.Month, 1),
      TariffModelTermId = catalogue.TariffModelTermId,
      MeasurementServiceFee =
        catalogue.Items
          .FirstOrDefault(item =>
            item.TariffElementTermId == TariffElement.SiteFeeTermId)
          .Price,
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
                    Consumption =
                      endEnergy -
                      beginEnergy,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endEnergy -
                      beginEnergy),
                  },
                TariffElement.HighCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.HighCostUsageTermId,
                    ValueFrom = beginHighCostEnergy,
                    ValueTo = endHighCostEnergy,
                    Consumption =
                      endHighCostEnergy -
                      beginHighCostEnergy,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                  },
                TariffElement.LowCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.LowCostUsageTermId,
                    ValueFrom = beginLowCostEnergy,
                    ValueTo = endLowCostEnergy,
                    Consumption =
                      endLowCostEnergy -
                      beginLowCostEnergy,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endLowCostEnergy -
                      beginLowCostEnergy),
                  },
                TariffElement.MaxPowerTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.MaxPowerTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Consumption = maxPower,
                    UnitPrice = item.Price,
                    Amount = item.Price * maxPower
                  },
                _ => null as ExpenditureItemData?,
              }
            )),
      SupplyExpenditure =
        ExpenditureData.FromItems(
          catalogue.Items.SelectFilter(
            item =>
              item.TariffElementTermId switch
              {
                TariffElement.EnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.SupplyTermId,
                    ValueFrom = beginEnergy,
                    ValueTo = endEnergy,
                    Consumption =
                      endEnergy -
                      beginEnergy,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endEnergy -
                      beginEnergy),
                  },
                TariffElement.HighCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.HighCostSupplyTermId,
                    ValueFrom = beginHighCostEnergy,
                    ValueTo = endHighCostEnergy,
                    Consumption =
                      endHighCostEnergy -
                      beginHighCostEnergy,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                  },
                TariffElement.LowCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.LowCostSupplyTermId,
                    ValueFrom = beginLowCostEnergy,
                    ValueTo = endLowCostEnergy,
                    Consumption =
                      endLowCostEnergy -
                      beginLowCostEnergy,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endLowCostEnergy -
                      beginLowCostEnergy),
                  },
                TariffElement.RenewableEnergyFeeTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.RenewableEnergyFeeTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Consumption =
                      (endEnergy -
                      beginEnergy) +
                      (endLowCostEnergy -
                      beginLowCostEnergy) +
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endEnergy -
                      beginEnergy) +
                      (endLowCostEnergy -
                      beginLowCostEnergy) +
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                  },
                TariffElement.BusinessUsageFeeTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.BusinessUsageFeeTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Consumption =
                      (endEnergy -
                      beginEnergy) +
                      (endLowCostEnergy -
                      beginLowCostEnergy) +
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (endEnergy -
                      beginEnergy) +
                      (endLowCostEnergy -
                      beginLowCostEnergy) +
                      (endHighCostEnergy -
                      beginHighCostEnergy),
                  },
                _ => null as ExpenditureItemData?
              })),
    };
}
