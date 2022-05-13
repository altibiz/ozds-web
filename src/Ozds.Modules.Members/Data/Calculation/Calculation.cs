using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Calculation : ContentPart
{
  public ContentPickerField Site { get; set; } = new();
  public TaxonomyField TariffModel { get; set; } = new();
  public DateField DateFrom { get; set; } = new();
  public DateField DateTo { get; set; } = new();
  public NumericField MeasurementServiceFee { get; set; } = new();
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
