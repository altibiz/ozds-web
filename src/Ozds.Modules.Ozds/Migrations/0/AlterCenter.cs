using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Ozds.M0;

public static partial class AlterCenter
{
  public static void AlterCenterType(
      this IContentDefinitionManager contentDefinitionManager) =>
    contentDefinitionManager.AlterTypeDefinition("Center",
      type => type
        .DisplayedAs("Zatvoreni distribucijski sustav")
        .Creatable()
        .Listable()
        .Draftable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithDescription("Naziv zatvorenog distribucijskog sustava")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("Center", "Center",
          part => part
            .WithPosition("2")
            .WithDisplayName("Centar")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Operator", "Person",
          part => part
            .WithDisplayName("Operator ZDS-a")
            .WithPosition("3")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        // NOTE: Owner clashes with the internal Orchard Core Owner field
        .WithPart("CenterOwner", "Person",
          part => part
            .WithDisplayName("Vlasnik")
            .WithPosition("4")
            .WithSettings(
              new FieldEditorSettings
              {
              }))
        .WithPart("Catalogues", "BagPart",
          part => part
            .WithDisplayName("Cjenik")
            .WithPosition("5")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes =
                  new[]
                  {
                    "Catalogue"
                  }
              }))
        .WithPart("Consumers", "ListPart",
          part => part
            .WithDisplayName("Korisnici")
            .WithPosition("6")
            .WithSettings(
              new ListPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Consumer"
                }
              })));

  public static void AlterCenterPart(
      this IContentDefinitionManager contentDefinitionManager) =>
    contentDefinitionManager.AlterPartDefinition("Center", part => { });
}
