using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterPrimarySite
{
  public static void AlterPrimarySiteType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("PrimarySite",
      type => type
        .DisplayedAs("Primarno obračunsko mjerno mjesto")
        .Creatable()
        .Listable()
        .Draftable()
        .Securable()
        .WithPart("PrimarySite",
          part => part
            .WithPosition("0")
            .WithSettings(
              new PrimarySiteSettings
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
        .WithPart("Site",
          part => part
            .WithPosition("2")
            .WithSettings(
              new SiteSettings
              {
              }))
        .WithPart("ListPart",
          part => part
            .WithDisplayName("Sekundarna obračunska mjerna mjesta")
            .WithPosition("3")
            .WithSettings(
              new ListPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "SecondarySite"
                }
              })));

  public static void AlterPrimarySitePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("PrimarySite", part => { });
}
