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
        .Draftable()
        .Versionable()
        .WithPart("TitlePart", part => part
          .WithPosition("0")
          .WithDisplayName("Naziv")
          .WithSettings(
            new TitlePartSettings
            {
              Options = TitlePartOptions.EditableRequired,
              Pattern =
              @"
                {%- assign catalogueItem = ContentItem.Content.CatalogueItem -%}
                {%- assign tariffs = catalogueItem.Tariff.TermContentIds -%}
                {%- assign tariff = tariffs[0] | content_item_id -%}
                {%- assign measurementUnits = catalogueItem.Unit.TermContentIds -%}
                {%- assign measurementUnit = measurementUnits[0] | content_item_id -%}
                {{- tariff }} {{ measurementUnit -}}
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
                Required = true,
                Unique = true
              }))
        .WithField("Currency",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Valuta")
            .WithPosition("2")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4098639c3zswm084zyay3je1m9",
                Required = true,
                Unique = true
              }))
        .WithField("Tax",
          field => field
            .OfType("NumericField")
            .WithDisplayName("PDV")
            .WithPosition("5")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Maximum = 1
              }))
        .WithField("Price",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Jedinična cijena")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              })));
}
