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
        .DisplayedAs("Stavka cjenika")
        .Creatable()
        .Listable()
        .Securable()
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
{%- assign tariffElements = catalogueItem.TariffElement.TermContentIds -%}
{%- assign tariffElement = tariffElements[0] | content_item_id -%}
{{- tariffElement -}}
              "
            }))
        .WithPart("CatalogueItem", part => part
          .WithPosition("2")
          .WithDisplayName("Stavka cjenika")
          .WithSettings(
            new FieldEditorSettings
            {
            })));

  public static void AlterCatalogueItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("CatalogueItem",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Stavka cjenika")
        .WithField("Tariff",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifni element")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4v6ax991xqge9wyk8g6z4e6vn4",
                Required = true,
                Unique = true
              }))
        .WithField("Price",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Cijena")
            .WithPosition("2")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2
              })));
}
