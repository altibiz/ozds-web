using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Modules.Ozds.M0;

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
        .WithPart("TitlePart",
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
{%- assign consumerName = receipt.Data.Consumer.Name -%}
{%- assign centerName = receipt.Data.CenterTitle -%}
{%- assign date = receipt.Date.Value | date: '%d. %m. %Y.' -%}
{{- centerName }} {{ consumerName }} {{ date -}}
                ",
              }))
        .WithPart("Receipt",
          part => part
            .WithPosition("2")));

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
        .WithField("DateFrom",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum od")
            .WithDescription("Datum početka mjernja")
            .WithPosition("3")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              }))
        .WithField("DateTo",
          field => field
            .OfType("DateField")
            .WithDisplayName("Datum do")
            .WithDescription("Datum kraja mjernja")
            .WithPosition("4")
            .WithSettings(
              new DateFieldSettings
              {
                Required = true
              })));
}
