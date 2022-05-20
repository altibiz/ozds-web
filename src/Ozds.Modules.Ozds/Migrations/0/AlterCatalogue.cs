using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Ozds.M0;

public static partial class AlterCatalogue
{
  public static void AlterCatalogueType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Catalogue",
      type => type
        .DisplayedAs("Cjenik")
        .Creatable()
        .Securable()
        .Draftable()
        .Versionable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.Editable,
                Pattern =
                @"
{%- assign catalogue = ContentItem.Content.Catalogue -%}
{%- assign tariffModel = catalogue.TariffModel | taxonomy_terms | first -%}
{{- tariffModel -}}
                "
              }))
        .WithPart("Catalogue",
          part => part
            .WithPosition("2")
            .WithDisplayName("Cjenik")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Items", "BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Stavke kataloga")
            .WithPosition("3")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "CatalogueItem"
                },
              })));

  public static void AlterCataloguePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Catalogue",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Tarifni model")
        .WithField("TariffModel",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tarifni model")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "42d7a5kashgdgztx5ehjb4deca",
                Required = true,
                Unique = true
              })));
}
