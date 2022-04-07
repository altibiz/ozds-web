using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Media.Settings;

namespace OrchardCore.Themes.OzdsTheme.M1;

public static partial class AlgerGPiece {
  public static void AlterGPieceType(this IContentDefinitionManager content) =>
      content.AlterTypeDefinition("GPiece", type => type.WithPart("GPiece"));

  public static void AlterGPiecePart(this IContentDefinitionManager
          content) => content.AlterPartDefinition("GPiece",
      cfg => cfg.WithDescription("Contains the fields for the current type")
                 .WithField(
                     "Caption", fieldBuilder => fieldBuilder.OfType("HtmlField")
                                                    .WithDisplayName("Caption")
                                                    .WithEditor("Wysiwyg"))
                 .WithField("DisplayCaption",
                     fieldBuilder => fieldBuilder.OfType("BooleanField")
                                         .WithDisplayName("Display Caption"))
                 .WithField("Image",
                     fieldBuilder => fieldBuilder.OfType("MediaField")
                                         .WithDisplayName("Image")
                                         .WithSettings(new MediaFieldSettings {
                                           Required = true, Multiple = false
                                         }))
                 .WithField("ImageClass",
                     fieldBuilder => fieldBuilder.OfType("TextField")
                                         .WithDisplayName("Image Class"))
                 .WithField("ImageAltText",
                     fieldBuilder => fieldBuilder.OfType("TextField")
                                         .WithDisplayName("Image Alt Text"))
                 .WithField(
                     "Link", fieldBuilder => fieldBuilder.OfType("TextField")
                                                 .WithDisplayName("Link")
                                                 .WithEditor("Url")));
}
