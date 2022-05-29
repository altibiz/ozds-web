using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Ozds;

public static class TariffItem
{
  public const string ContentItemId = "4van7f3sda11fx2nm0pbqjef45";
  public const string UsageTermId = "42jhr1xpjvfpg6t02d014nr4xy";
  public const string HighCostUsageTermId = "4pmasmhzk65113a9svxxvj4r55";
  public const string LowCostUsageTermId = "4r7f2004nhpmzx0mr5xer6a44t";
  public const string MaxPowerTermId = "4mpfrnnvf0newswdsvvq9tpcxh";
  public const string MeasurementServiceFeeTermId = "4gvhahd4x59fv7gektpbqf48wa";
  public const string SupplyTermId = "454jn23baxytmxnzvq1wy6hvma";
  public const string HighCostSupplyTermId = "4zsh3gffhw1gn2ba9fj945m7cv";
  public const string LowCostSupplyTermId = "4nbab8htaqysb0v5cjy4jx235m";
  public const string RenewableEnergyFeeTermId = "4dnr256xfy2mr6095cfay03dc5";
  public const string BusinessUsageFeeTermId = "42aw5mm1t3cfh2mef7shta9qnw";

  public static Task<TariffTagType?> GetTariffItem(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TariffTagType>(ContentItemId, termId);

  public static Task<TariffTagType?> GetTariffItem(
      this TaxonomyCacheService taxonomy,
      TaxonomyField field) =>
    taxonomy.GetTariffItem(field.TermContentItemIds.First());

  public static bool IsUsage(string tariffItemTermId) =>
    s_usageTariffItemTermIds.Contains(tariffItemTermId);


  private static readonly IList<string> s_usageTariffItemTermIds =
    new List<string>
    {
      UsageTermId,
      HighCostUsageTermId,
      LowCostUsageTermId,
      MaxPowerTermId,
      MeasurementServiceFeeTermId
    };

  public static bool IsSupply(string tariffItemTermId) =>
    s_supplyTariffItemTermIds.Contains(tariffItemTermId);


  private static readonly IList<string> s_supplyTariffItemTermIds =
    new List<string>
    {
      SupplyTermId,
      HighCostSupplyTermId,
      LowCostSupplyTermId
    };
}
