using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Html.Settings;

namespace Ozds.Themes.Ozds.M0;

public static partial class AlterPage
{
  public static void AlterPageType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Page",
      type => type
        .DisplayedAs("Stranica")
        .Creatable()
        .Listable()
        .Securable()
        .Draftable()
        .Versionable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithDescription("Naziv stranice")
            .WithPosition("0")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = false,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("Page",
          part => part
            .WithPosition("1")
            .WithDisplayName("Stranica"))
        .WithPart("HtmlBodyPart",
          part => part
            .WithPosition("2")
            .WithDisplayName("Tijelo")
            .WithDescription("Tijelo stranice")
            .WithEditor("Wysiwyg")
            .WithSettings(
              new HtmlBodyPartSettings
              {
              })));

  public static void AlterPagePart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Page", part => { });
}
