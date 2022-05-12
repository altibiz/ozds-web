using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class Expenditure : ContentPart
{
  public NumericField InTotal { get; set; } = new();
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
};
