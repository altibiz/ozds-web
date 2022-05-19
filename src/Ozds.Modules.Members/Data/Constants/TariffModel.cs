using OrchardCore.Taxonomies.Fields;

namespace Ozds.Modules.Members;

public static class TariffModel
{
  public const string ContentItemId = "42d7a5kashgdgztx5ehjb4deca";
  public const string WhiteHighVoltageTermId = "4g4m7xhxj84na3xf49ks1ty7d5";
  public const string WhiteMediumVoltageTermId = "46k372fszexy61xan98qkz42w0";
  public const string BlueTermId = "41vr9sy8ap5ntvbkj74zbw6749";
  public const string WhiteLowVoltageTermId = "4w9hx6bgrmj70wdky9pcp9e1mz";
  public const string RedTermId = "4bypnkrf7e4ng3q59x732vgq5t";
  public const string YellowTermId = "48pb3v35bcfnbswk47q3nfm2dg";

  public static Task<TagType?> GetTariffModel(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TagType>(ContentItemId, termId);

  public static Task<TagType?> GetTariffModel(
      this TaxonomyCacheService taxonomy,
      TaxonomyField field) =>
    taxonomy.GetTariffModel(field.TermContentItemIds[0]);
}
