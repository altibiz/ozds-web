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
      decimal energyBegin,
      decimal energyEnd,
      decimal highCostEnergyBegin,
      decimal highCostEnergyEnd,
      decimal lowCostEnergyBegin,
      decimal lowCostEnergyEnd,
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
                    ValueFrom = energyBegin,
                    ValueTo = energyEnd,
                    Consumption =
                      energyEnd -
                      energyBegin,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (energyEnd -
                      energyBegin),
                  },
                TariffElement.HighCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.HighCostUsageTermId,
                    ValueFrom = highCostEnergyBegin,
                    ValueTo = highCostEnergyEnd,
                    Consumption =
                      highCostEnergyEnd -
                      highCostEnergyBegin,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (highCostEnergyEnd -
                      highCostEnergyBegin),
                  },
                TariffElement.LowCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.LowCostUsageTermId,
                    ValueFrom = lowCostEnergyBegin,
                    ValueTo = lowCostEnergyEnd,
                    Consumption =
                      lowCostEnergyEnd -
                      lowCostEnergyBegin,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (lowCostEnergyEnd -
                      lowCostEnergyBegin),
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
                    ValueFrom = energyBegin,
                    ValueTo = energyEnd,
                    Consumption =
                      energyEnd -
                      energyBegin,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (energyEnd -
                      energyBegin),
                  },
                TariffElement.HighCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.HighCostSupplyTermId,
                    ValueFrom = highCostEnergyBegin,
                    ValueTo = highCostEnergyEnd,
                    Consumption =
                      highCostEnergyEnd -
                      highCostEnergyBegin,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (highCostEnergyEnd -
                      highCostEnergyBegin),
                  },
                TariffElement.LowCostEnergyTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.LowCostSupplyTermId,
                    ValueFrom = lowCostEnergyBegin,
                    ValueTo = lowCostEnergyEnd,
                    Consumption =
                      lowCostEnergyEnd -
                      lowCostEnergyBegin,
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (lowCostEnergyEnd -
                      lowCostEnergyBegin),
                  },
                TariffElement.RenewableEnergyFeeTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.RenewableEnergyFeeTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Consumption =
                      (energyEnd -
                      energyBegin) +
                      (lowCostEnergyEnd -
                      lowCostEnergyBegin) +
                      (highCostEnergyEnd -
                      highCostEnergyBegin),
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (energyEnd -
                      energyBegin) +
                      (lowCostEnergyEnd -
                      lowCostEnergyBegin) +
                      (highCostEnergyEnd -
                      highCostEnergyBegin),
                  },
                TariffElement.BusinessUsageFeeTermId =>
                  new ExpenditureItemData
                  {
                    TariffItemTermId = TariffItem.BusinessUsageFeeTermId,
                    ValueFrom = default,
                    ValueTo = default,
                    Consumption =
                      (energyEnd -
                      energyBegin) +
                      (lowCostEnergyEnd -
                      lowCostEnergyBegin) +
                      (highCostEnergyEnd -
                      highCostEnergyBegin),
                    UnitPrice = item.Price,
                    Amount = item.Price *
                      (energyEnd -
                      energyBegin) +
                      (lowCostEnergyEnd -
                      lowCostEnergyBegin) +
                      (highCostEnergyEnd -
                      highCostEnergyBegin),
                  },
                _ => null as ExpenditureItemData?
              })),
    };
}
