using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Media.Settings;

namespace Members.M0;

public static class AlterImage
{
  public static void AlterImageType(this IContentDefinitionManager content) =>
      content.AlterTypeDefinition("Image",
          type => type.DisplayedAs("Image")
                      .Draftable()
                      .Versionable()
                      .Securable()
                      .Stereotype("Widget")
                      .WithPart("Image", part => part.WithPosition("0")));

  public static void AlterImagePart(this IContentDefinitionManager
          content) => content.AlterPartDefinition("Image",
      part =>
          part.WithField(
                  "Media", field => field.OfType("MediaField")
                                        .WithDisplayName("Image")
                                        .WithPosition("0")
                                        .WithSettings(new MediaFieldSettings
                                        {
                                          Multiple = false,
                                          Required = true,
                                        }))
              .WithField("Caption",
                  field =>
                      field.OfType("TextField")
                          .WithDisplayName("Caption")
                          .WithPosition("2")
                          .WithSettings(new TextFieldSettings
                          {
                            Hint =
                                "A description of the image used as title or alternate text",
                          }))
              .WithField(
                  "Link", field => field.OfType("LinkField")
                                       .WithDisplayName("Link")
                                       .WithPosition("1")
                                       .WithSettings(new LinkFieldSettings
                                       {
                                         LinkTextMode = LinkTextMode.Url,
                                       })));
}
