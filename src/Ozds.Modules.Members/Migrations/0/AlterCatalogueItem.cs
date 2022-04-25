using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;

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
        .WithPart("CatalogueItem", part => part
          .WithPosition("0")
          .WithSettings(
            new CatalogueItemSettings
            {
            }))
        .WithPart("TitlePart", part => part
          .WithPosition("1")
          .WithDisplayName("Naziv")
          .WithSettings(
            new TitlePartSettings
            {
              Options = TitlePartOptions.GeneratedHidden,
              Pattern =
              @"
                {%- assign catalogueItem = ContentItem.Content.CatalogueItem -%}
                {%- assign tariffs = catalogueItem.Tariff.TermContentIds -%}
                {%- assign tariff = tariffs[0] | content_item_id -%}
                {%- assign measurementUnits = catalogueItem.Unit.TermContentIds -%}
                {%- assign measurementUnit = measurementUnits[0] | content_item_id -%}
                {{- tariff }} {{ measurementUnit -}}
              "
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
            .WithPosition("1")
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
            .WithPosition("2")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              })));
}
