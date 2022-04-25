using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Media.Settings;

namespace Ozds.Modules.Members.M0;

public static class AlterImage
{
  public static void AlterImageType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Image",
      type => type
        .DisplayedAs("Slika")
        .Draftable()
        .Versionable()
        .Securable()
        .Stereotype("Widget")
        .WithPart("Image",
          part => part
            .WithPosition("0")));

  public static void AlterImagePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Image",
      part => part
        .WithField("Media",
          field => field
            .OfType("MediaField")
            .WithDisplayName("Slika")
            .WithPosition("0")
            .WithSettings(
              new MediaFieldSettings
              {
                Multiple = false,
                Required = true,
              }))
        .WithField("Caption",
          field => field
            .OfType("TextField")
            .WithDisplayName("Podnaslov")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Hint = "Opis slike koristen kao podnaslov",
              }))
        .WithField("Link",
          field => field
            .OfType("LinkField")
            .WithDisplayName("Link")
            .WithPosition("2")
            .WithSettings(
              new LinkFieldSettings
              {
                LinkTextMode = LinkTextMode.Url,
              })));
}
