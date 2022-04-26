using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;
using OrchardCore.Autoroute.Models;
using OrchardCore.Alias.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCatalogue
{
  public static void AlterCatalogueType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Catalogue",
      type => type
        .DisplayedAs("Katalog")
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
            .WithDisplayName("Katalog")
            .WithSettings(
              new CatalogueSettings
              {
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithDisplayName("Ruta")
            .WithDisplayName("Automatski generirana ruta kataloga")
            .WithPosition("2")
            .WithSettings(
              new AutoroutePartSettings
              {
                AllowRouteContainedItems = true,
                ManageContainedItemRoutes = true,
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              }))
        .WithPart("AliasPart",
          part => part
            .WithDisplayName("Alias")
            .WithPosition("3")
            .WithSettings(
              new AliasPartSettings
              {
                Options = AliasPartOptions.Editable,
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              }))
        .WithPart("ListPart",
          part => part
            .WithDisplayName("Stavke")
            .WithDescription("Stavke kataloga")
            .WithPosition("4")
            .WithSettings(
              new ListPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "CatalogueItem"
                },
              })));

  public static void AlterCataloguePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Catalogue",
        part => part
          .WithDisplayName("Katalog")
          .WithDescription("Opis kataloga")
          .WithField("Description",
            field => field
              .OfType("TextField")
              .WithDisplayName("Opis")
              .WithEditor("Textarea")
              .WithSettings(
                new TextFieldSettings
                {
                })));
}
