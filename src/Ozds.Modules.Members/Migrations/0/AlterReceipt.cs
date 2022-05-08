using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceipt
{
  public static void AlterReceiptType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Receipt",
      type => type
        .DisplayedAs("Račun")
        .Creatable()
        .Listable()
        .Draftable()
        .Securable()
         .WithPart("Title", "TitlePart",
          part => part
            .WithDisplayName("Naslov")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign receipt = ContentItem.Content.Receipt -%}
{%- assign consumer = ContentItem.Content.Consumer -%}
{%- assign consumerName = consumer.Name.Text -%}
{%- assign date = receipt.Date.Value | date: '%d. %m. %Y.' -%}
{{- consumerName }} {{ date -}}
                ",
              }))
        .WithPart("Center", "Person",
          part => part
            .WithDisplayName("Operator")
            .WithPosition("2")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Consumer", "Person",
          part => part
            .WithDisplayName("Korisnik ZDS-a")
            .WithPosition("3")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Calculation", "BagPart",
          part => part
            .WithDisplayName("Obračun")
            .WithPosition("4")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes =
                new[]
                {
                  "Calculation"
                }
              }))
        .WithPart("Items", "BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithPosition("5")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes =
                  new[]
                  {
                    "ReceiptItem"
                  }
              }))
        .WithPart("Receipt",
          part => part
            .WithPosition("5")
            .WithSettings(
              new FieldEditorSettings
              {
              })));

  public static void AlterReceiptPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Receipt",
      part => part
        .WithDisplayName("Račun")
        .WithField("Site",
          field => field
            .OfType("ContentPickerField")
            .WithDisplayName("Obračunsko mjerno mjesto")
            .WithPosition("1")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Required = true,
                Multiple = false,
                DisplayedContentTypes =
                new[]
                {
                  "SecondarySite"
                }
              }))
        .WithField("Date",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum izrade")
            .WithPosition("2")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("UKUPNO")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2
              }))
        .WithField("Tax",
          field => field
            .OfType("NumericField")
            .WithDisplayName("PDV (13%)")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2,
              }))
        .WithField("InTotalWithTax",
          field => field
            .OfType("NumericField")
            .WithDisplayName("UKUPNI IZNOS")
            .WithPosition("5")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
                Scale = 2,
              })));
}
