using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterAdminPage
{
  public static void AlterAdminPageType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("AdminPage",
      type => type
        .DisplayedAs("Administratorska stranica")
        .Creatable()
        .Listable()
        .Draftable()
        .Versionable()
        .Securable()
        .WithPart("AdminPage",
          part => part
            .WithPosition("0"))
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naziv")
            .WithSettings(
              new TitlePartSettings
              {
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("LiquidPart",
          part => part
            .WithPosition("2")));
}
