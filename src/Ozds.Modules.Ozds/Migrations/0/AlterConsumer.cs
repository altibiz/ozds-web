using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;

namespace Ozds.Modules.Ozds.M0;

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
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign person = ContentItem.Content.Person -%}
{%- assign name = person.Name.Text -%}
{{- name -}}
                ",
              }))
        .WithPart("Consumer",
          part => part
            .WithPosition("2")
            .WithDisplayName("Korisnik ZDS-a")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Person",
          part => part
            .WithDisplayName("Osoba")
            .WithPosition("3")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("SecondarySites", "ListPart",
          part => part
            .WithDisplayName("Sekundarna obračunska mjerna mjesta")
            .WithPosition("4")
            .WithSettings(
              new ListPartSettings
              {
                ContainedContentTypes =
                  new[]
                  {
                    "SecondarySite"
                  }
              })));

  public static void AlterConsumerPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Consumer",
      part => part
        .WithField("User",
          field => field
            .OfType("UserPickerField")
            .WithDisplayName("Korisnik")
            .WithPosition("1")
            .WithSettings(
              new UserPickerFieldSettings
              {
                Required = true,
              })));
}
