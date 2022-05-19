using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterSecondarySite
{
  public static void AlterSecondarySiteType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("SecondarySite",
      type => type
        .DisplayedAs("Sekundarno obračunsko mjerno mjesto")
        .Creatable()
        .Listable()
        .Draftable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign site = ContentItem.Content.Site -%}
{%- assign source = site.Source | taxonomy_terms | first -%}
{%- assign deviceId = site.DeviceId.Text -%}
{{- source }} {{ deviceId -}}
                ",
              }))
        .WithPart("SecondarySite",
          part => part
            .WithPosition("2")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Site",
          part => part
            .WithDisplayName("Obračunsko mjerno mjesto")
            .WithPosition("3")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Catalogue", "BagPart",
          part => part
            .WithDisplayName("Cjenik")
            .WithPosition("4")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes =
                new[]
                {
                  "Catalogue"
                }
              })));

  public static void AlterSecondarySitePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("SecondarySite", part => { });
}
