using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;

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
        .WithPart("SecondarySite",
          part => part
            .WithPosition("0")
            .WithSettings(
              new SecondarySiteSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("SitePart",
          part => part
            .WithDisplayName("Obračunsko mjerno mjesto")
            .WithPosition("2")
            .WithSettings(
              new SiteSettings
              {
              })));

  public static void AlterSecondarySitePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("SecondarySite", part => { });
}
