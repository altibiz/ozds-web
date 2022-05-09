using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceipt
{
  public static void AlterReceiptTypeRevision(
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
        .WithPart("Receipt",
          part => part
            .WithPosition("2")
            .WithSettings(
              new FieldEditorSettings
              {
              })));

  public static void AlterReceiptPartRevision(
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
              })));
}
