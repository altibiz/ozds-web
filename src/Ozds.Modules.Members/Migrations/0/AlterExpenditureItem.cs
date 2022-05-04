using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;

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
        .WithPart("TitlePart", part => part
          .WithPosition("1")
          .WithDisplayName("Naziv")
          .WithSettings(
            new TitlePartSettings
            {
              Options = TitlePartOptions.GeneratedHidden,
              Pattern =
              @"
{%- assign expenditureItem = ContentItem.Content.ExpenditureItem -%}
{%- assign tariffItemId = expenditureItem.TariffItem.TermContentIds[0] -%}
{%- assign tariffItem = tariffItemId | content_item_id -%}
{{- tariffItem -}}
              "
            }))
        .WithPart("ExpenditureItem",
          part => part
          .WithPosition("2")
          .WithSettings(
            new FieldEditorSettings
            {
            })));

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
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4van7f3sda11fx2nm0pbqjef45",
                Required = true,
                Unique = true
              }))
        .WithField("ValueFrom",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Stanje od")
            .WithPosition("2")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = false,
                Minimum = 0,
                // NOTE: as precise as possible because of regulations
                Scale = 10
              }))
        .WithField("ValueTo",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Stanje do")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = false,
                Minimum = 0,
                // NOTE: as precise as possible because of regulations
                Scale = 10
              }))
        .WithField("Consumption",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Potrošak")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 0
              }))
        .WithField("UnitPrice",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Jedinična cijena")
            .WithPosition("5")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2
              }))
        .WithField("Amount",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Iznos")
            .WithPosition("6"))
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2
              }));
}
