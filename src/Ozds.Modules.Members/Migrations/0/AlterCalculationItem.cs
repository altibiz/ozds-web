using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCalculationItem
{
  public static void AlterCalculationItemType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("CalculationItem",
      type => type
        .DisplayedAs("Stavka obračuna")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("CalculationItem",
          part => part
          .WithPosition("0")
          .WithSettings(
            new CalculationItemSettings
            {
            })));

  public static void AlterCalculationItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("CalculationItem",
      part => part
        .Attachable()
        .WithDisplayName("Stavka obračuna")
        .WithDescription("Tarifna stavka mjesečnog obračuna")
        .WithField("Tariff",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifa")
            .WithPosition("0")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "46nrgz0a0y570tcgvh50tq1vxp",
                Required = true,
                Unique = true
              }))
        .WithField("ValueFrom",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Stanje od")
            .WithPosition("1")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("ValueTo",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Stanje do")
            .WithPosition("2")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Status",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Status")
            .WithPosition("3")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4pxxf7kz2v2mgy67ehbhz5gsxq",
                Required = true,
                Unique = true
              }))
        .WithField("Constant",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Konstanta")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Unit",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Mjerna jedinica")
            .WithPosition("5")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4cqf2eeqqwadb4xechw3tbbsn0",
                Unique = true
              }))
        .WithField("Consumption",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Potrošak")
            .WithPosition("6")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("UnitPrice",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Jedinična cijena")
            .WithPosition("7")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Amount",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Iznos")
            .WithPosition("8"))
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }));
}
