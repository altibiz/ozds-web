using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Autoroute.Models;

namespace Ozds.Modules.Ozds.M0;

public static partial class AlterTariffTag
{
  public static void AlterTariffTagType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("TariffTag",
      type => type
        .DisplayedAs("Tarifna značajka")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naziv")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign tariffElement = ContentItem.Content.TariffElement -%}
{%- assign name = tariffElement.Name.Text -%}
{%- assign abbreviation = tariffElement.Abbreviation.Text -%}
{%- if abbreviation -%}
  {{- name }} - {{ abbreviation -}}
{%- else -%}
  {{- name -}}
{%- endif -%}
                "
              }))
        .WithPart("TariffTag",
          part => part
            .WithPosition("2")
            .WithDisplayMode("")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithPosition("3")
            .WithDisplayName("Ruta")
            .WithDescription("Automatski generirana ruta tarifnog elementa")
            .WithSettings(
              new AutoroutePartSettings
              {
                Pattern =
                @"
{%- assign title = ContentItem.Content.TitlePart -%}
{{- title.Title | slugify -}}
                "
              })));

  public static void AlterTariffTagPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("TariffTag",
      part => part
        .WithField("Name",
          field => field
            .OfType("TextField")
            .WithDisplayName("Ime")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Abbreviation",
          field => field
            .OfType("TextField")
            .WithDisplayName("Kratica")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("Unit",
          field => field
            .OfType("TextField")
            .WithDisplayName("Jedinica")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              })));
}
