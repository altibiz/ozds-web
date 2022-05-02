using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Autoroute.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterTariffElement
{
  public static void AlterTariffElementType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("TariffElement",
      type => type
        .DisplayedAs("Tarifni element")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithPosition("0")
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
{%- assign unit = tariffElement.Unit.Text -%}
{{- name }} {{ abbreviation -}}
                "
              }))
        .WithPart("TariffElement",
          part => part
            .WithPosition("1")
            .WithDisplayMode("")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithPosition("2")
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

  public static void AlterTariffElementPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("TariffElement",
      part => part
        .WithField("Name",
          field => field
            .OfType("TextField")
            .WithDisplayName("Ime")
            .WithPosition("0")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Abbreviation",
          field => field
            .OfType("TextField")
            .WithDisplayName("Kratica")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("Unit",
          field => field
            .OfType("TextField")
            .WithDisplayName("Jedinica")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              })));
}
