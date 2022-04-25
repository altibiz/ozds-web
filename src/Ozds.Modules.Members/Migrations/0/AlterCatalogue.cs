using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
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
        .Listable()
        .Securable()
        .WithPart("Catalogue",
          part => part
            .WithPosition("0")
            .WithSettings(
              new CatalogueSettings
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
    content.AlterPartDefinition("Catalogue", part => { });
}
