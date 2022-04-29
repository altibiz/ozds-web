using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCatalogue
{
  public static void AlterCatalogueType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Catalogue",
      type => type
        .DisplayedAs("Cjenik")
        .Creatable()
        .Securable()
        .Draftable()
        .Versionable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithPosition("0")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("Catalogue",
          part => part
            .WithPosition("1")
            .WithDisplayName("Cjenik")
            .WithSettings(
              new CatalogueSettings
              {
              }))
        .WithPart("BagPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Stavke kataloga")
            .WithPosition("2")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "CatalogueItem"
                },
              })));

  public static void AlterCataloguePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Catalogue", part => { });
}
