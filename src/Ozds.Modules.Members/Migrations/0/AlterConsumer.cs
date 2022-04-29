using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterConsumer
{
  public static void AlterConsumerType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Consumer",
      type => type
        .DisplayedAs("Korisnik ZDS-a")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithDisplayName("Naziv člana")
            .WithPosition("0")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
                Pattern =
                @"
{%- assign person = ContentItem.Content.Person -%}
{%- assign name = person.Name.Text -%}
{{- name -}}
                ",
              }))
        .WithPart("Consumer",
          part => part
            .WithPosition("1")
            .WithDisplayName("Korisnik ZDS-a"))
        .WithPart("Person",
          part => part
            .WithDisplayName("Osoba")
            .WithPosition("2")
            .WithSettings(
              new PersonSettings
              {
              })));

  public static void AlterConsumerPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Consumer",
      part => part
        .WithField("SecondarySites",
          part => part
            .OfType("ContentPickerField")
            .WithDisplayName("Sekundarna obračunska mjerna mjesta")
            .WithPosition("0")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Required = true,
                Multiple = true,
                DisplayedContentTypes = new[]
                {
                  "SecondarySite"
                }
              })));
}
