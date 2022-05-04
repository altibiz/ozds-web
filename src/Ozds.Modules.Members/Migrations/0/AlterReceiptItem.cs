using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceiptItem
{
  public static void AlterReceiptItemType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("ReceiptItem",
      type => type
        .DisplayedAs("Stavka računa")
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
{%- assign receiptItem = ContentItem.Content.receiptItem -%}
{%- assign articleId = receiptItem.Article.TermContentIds[0] -%}
{%- assign article = articleId | content_item_id -%}
{%- assign ordinalNumber = receiptItem.OrdinalNumber.Text -%}
{{- ordinalNumber }} {{ article -}}
              "
            }))
        .WithPart("ReceiptItem",
          part => part
            .WithPosition("1")
            .WithSettings(
              new FieldEditorSettings
              {
              })));

  public static void AlterReceiptItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("ReceiptItem",
      part => part
        .Attachable()
        .WithDisplayName("Stavka računa")
        .WithField("OrdinalNumber",
          field => field
            .OfType("TextField")
            .WithDisplayName("Redni broj")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Article",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Artikl")
            .WithPosition("2")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4van7f3sda11fx2nm0pbqjef45",
                Unique = true,
                Required = true
              }))
        .WithField("Amount",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Količina")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Minimum = 0,
                Scale = 0
              }))
        .WithField("Price",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Cijena")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Minimum = 0,
                // NOTE: as precise as possible because of regulations
                Scale = 10
              }))
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno")
            .WithPosition("5")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2
              })));
}
