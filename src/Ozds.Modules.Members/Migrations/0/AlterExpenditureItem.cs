using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterExpenditureItem
{
  public static void AlterExpenditureItemType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("ExpenditureItem",
      type => type
        .DisplayedAs("Stavka troška")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("ExpenditureItem",
          part => part
          .WithPosition("0")));

  public static void AlterExpenditureItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("ExpenditureItem",
      part => part
        .Attachable()
        .WithDisplayName("Stavka troška")
        .WithField("TariffItem",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifna stavka")
            .WithPosition("0")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "",
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
                Required = false
              }))
        .WithField("ValueTo",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Stanje do")
            .WithPosition("2")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = false
              }))
        .WithField("Consumption",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Potrošak")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("UnitPrice",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Jedinična cijena")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Amount",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Iznos")
            .WithPosition("5"))
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }));
}
