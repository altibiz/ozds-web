using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;

namespace OrchardCore.Themes.OzdsTheme.M1;

public static partial class AlterGallery {
  public static void AlterGalleryType(
      this IContentDefinitionManager ContentDefinitionManager) =>
      ContentDefinitionManager.AlterTypeDefinition("Gallery",
          type =>
              type.WithPart("TitlePart")
                  .WithPart("Gallery")
                  .WithPart("GPieces", "BagPart",
                      cfg => cfg.WithDisplayName("GPieces")
                                 .WithDescription("GPieces to display in the.")
                                 .WithSettings(new BagPartSettings {
                                   ContainedContentTypes = new[] { "GPiece" },
                                   DisplayType = "Detail"
                                 }))
                  .Stereotype("Widget"));

  public static void AlterGalleryPart(
      this IContentDefinitionManager ContentDefinitionManager) =>
      ContentDefinitionManager.AlterPartDefinition("Gallery",
          cfg => cfg.WithDescription("Contains the fields for the current type")
                     .WithField("DisplayType",
                         fieldBuilder => fieldBuilder.OfType("TextField")
                                             .WithDisplayName("Display Type")));
}
