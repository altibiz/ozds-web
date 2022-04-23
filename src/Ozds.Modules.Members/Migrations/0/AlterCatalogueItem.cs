using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCatalogueItem
{
  public static void AlterCatalogueItemType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("CatalogueItem",
      type => type
        .DisplayedAs("Stavka kataloga")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("CatalogueItem",
          part => part
          .WithPosition("0")
          .WithSettings(
            new CatalogueItemSettings
            {
            })));

  public static void AlterCatalogueItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("CatalogueItem",
      part => part
        .Attachable()
        .WithDisplayName("Stavka kataloga")
        .WithDescription("Cijena i mjerna jedinica u određenoj tarifi")
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
        .WithField("Unit",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Mjerna jedinica")
            .WithPosition("2")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4cqf2eeqqwadb4xechw3tbbsn0",
                Unique = true
              }))
        .WithField("UnitPrice",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Jedinična cijena")
            .WithPosition("6")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              })));
}
