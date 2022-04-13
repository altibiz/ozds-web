using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCenter
{
  public static void AlterCenterType(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition("Center",
          type =>
              type.DisplayedAs("Zatvoreni distribucijski sustav")
                  .Creatable()
                  .Listable()
                  .Draftable()
                  .Securable()
                  .WithPart("PersonPart",
                      part => part.WithPosition("0").WithSettings(new
                      {
                        Type = 1,
                      }))
                  .WithPart("Center", part => part.WithPosition("2"))
                  .WithPart("AliasPart", part => part.WithPosition("3"))
                  .WithPart("TitlePart",
                      part => part.WithPosition("1").WithSettings(
                          new TitlePartSettings
                          {
                            Options = TitlePartOptions.GeneratedDisabled,
                            Pattern =
                                "{{ ContentItem.Content.PersonPart.Title.Text }}",
                          })));

  public static void AlterCenterPart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition(
          "Center", part => part.WithField("Representative",
                        field => field.OfType("TextField")
                                     .WithDisplayName("Zastupna osoba")
                                     .WithPosition("0")));
}
