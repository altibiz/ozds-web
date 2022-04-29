using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCenter
{
  public static void AlterCenterType(
      this IContentDefinitionManager contentDefinitionManager) =>
    contentDefinitionManager.AlterTypeDefinition("Center",
      type => type
        .DisplayedAs("Zatvoreni distribucijski sustav")
        .Creatable()
        .Listable()
        .Draftable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithDescription("Naziv zatvorenog distribucijskog sustava")
            .WithPosition("0")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedHidden,
                Pattern =
                @"
{%- assign owner = ContentItem.Content.Owner -%}
{%- assign name = owner.Name.Text -%}
{{- name -}}
                ",
              }))
        .WithPart("Center",
          part => part
            .WithPosition("1")
            .WithDisplayName("Centar")
            .WithSettings(
              new CenterSettings
              {
              }))
        .WithPart("Operator", "Person",
          part => part
            .WithDisplayName("Operator")
            .WithPosition("2")
            .WithSettings(
              new PersonSettings
              {
              }))
        .WithPart("Owner", "Person",
          part => part
            .WithDisplayName("Vlasnik")
            .WithPosition("3")
            .WithSettings(
              new PersonSettings
              {
              }))
        .WithPart("ListPart",
          part => part
            .WithDisplayName("Korisnici")
            .WithPosition("4")
            .WithSettings(
              new ListPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Consumer"
                }
              })));

  public static void AlterCenterPart(
      this IContentDefinitionManager contentDefinitionManager) =>
    contentDefinitionManager.AlterPartDefinition("Center", part => { });
}
