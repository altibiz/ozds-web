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
        .DisplayedAs("Znacajka")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("Tag",
          part => part
            .WithPosition("0"))
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naziv")
            .WithDisplayName("Naziv znacajke")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithPosition("2")
            .WithDisplayName("Ruta")
            .WithDescription("Automatski generirana ruta znacajke")
            .WithSettings(
              new AutoroutePartSettings
              {
                Pattern = @"{{ ContentItem.Content.TitlePart.Text | slugify }}"
              })));

  public static void AlterTagPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Tag", part => { });
}
