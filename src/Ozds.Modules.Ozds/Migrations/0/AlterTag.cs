using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;
using OrchardCore.Alias.Settings;

namespace Ozds.Modules.Ozds.M0;

public static partial class AlterTag
{
  public static void AlterTagType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Tag",
      type => type
        .DisplayedAs("Značajka")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naziv značajke")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("Tag",
          part => part
            .WithPosition("2")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithPosition("3")
            .WithDisplayName("Ruta")
            .WithDescription("Automatski generirana ruta značajke")
            .WithSettings(
              new AutoroutePartSettings
              {
                Pattern =
                @"
{%- assign title = ContentItem.Content.TitlePart -%}
{{- title.Title | slugify -}}
                "
              }))
        .WithPart("AliasPart",
          part => part
            .WithPosition("4")
            .WithDisplayName("Alias")
            .WithDescription("Automatski generiran alias značajke")
            .WithSettings(
              new AliasPartSettings
              {
                Options = AliasPartOptions.GeneratedDisabled,
                Pattern =
                @"
{%- assign title = ContentItem.Content.TitlePart -%}
{{- title.Title | slugify -}}
                "
              })));

  public static void AlterTagPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Tag", part => { });
}
