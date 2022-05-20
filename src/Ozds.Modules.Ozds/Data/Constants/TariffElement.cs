using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Ozds;

public static class TariffElement
{
  public const string ContentItemId = "4v6ax991xqge9wyk8g6z4e6vn4";
  public const string EnergyTermId = "44vg6aarzp3ayv2praq3t17pce";
  public const string HighCostEnergyTermId = "4mw0ncthn1aq4r5k6cqfk19yw3";
  public const string LowCostEnergyTermId = "4cqexne8f6j3sv77ewvydhxzn8";
  public const string MaxPowerTermId = "4fpzdpa1c1mdjsef4j21wnd9x7";
  public const string SiteFeeTermId = "4eexj7kgkdww3rpcavx5mxw3da";
  public const string RenewableEnergyFeeTermId = "renewable";
  public const string BusinessUsageFeeTermId = "business";

  public static Task<TariffTagType?> GetTariffElement(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TariffTagType>(ContentItemId, termId);

  public static Task<TariffTagType?> GetTariffElement(
      this TaxonomyCacheService taxonomy,
      TaxonomyField field) =>
    taxonomy.GetTariffElement(field.TermContentItemIds[0]);
}
