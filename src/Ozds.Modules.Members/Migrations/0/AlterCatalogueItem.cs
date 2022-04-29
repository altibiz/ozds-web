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
        .WithPart("TitlePart", part => part
          .WithPosition("0")
          .WithDisplayName("Naziv")
          .WithSettings(
            new TitlePartSettings
            {
              Options = TitlePartOptions.GeneratedHidden,
              Pattern =
              @"
{%- assign catalogueItem = ContentItem.Content.CatalogueItem -%}
{%- assign tariffElements = catalogueItem.TariffElement.TermContentIds -%}
{%- assign tariffElement = tariffElements[0] | content_item_id -%}
{{- tariffElement -}}
              "
            }))
        .WithPart("CatalogueItem", part => part
          .WithPosition("1")
          .WithDisplayName("Stavka kataloga")
          .WithSettings(
            new CatalogueItemSettings
            {
            })));

  public static void AlterCatalogueItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("CatalogueItem",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Stavka kataloga")
        .WithDescription("Cijena određenog tarifnog elementa")
        .WithField("Tariff",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifni element")
            .WithPosition("0")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "46nrgz0a0y570tcgvh50tq1vxp",
                Required = true,
                Unique = true
              }))
        .WithField("Price",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Cijena")
            .WithPosition("1")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              })));
}
