using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;

namespace Ozds.Modules.Members.M0;

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
            .WithPosition("0")
            .WithDisplayName("Naziv")
            .WithDisplayName("Naziv značajke")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("Tag",
          part => part
            .WithPosition("1"))
        .WithPart("AutoroutePart",
          part => part
            .WithPosition("2")
            .WithDisplayName("Ruta")
            .WithDescription("Automatski generirana ruta značajke")
            .WithSettings(
              new AutoroutePartSettings
              {
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              })));

  public static void AlterTagPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Tag", part => { });
}
