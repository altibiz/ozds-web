using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;
using OrchardCore.Flows.Models;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Alias.Settings;
using OrchardCore.Autoroute.Models;

namespace Ozds.Modules.Members.M0;

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
        .WithPart("Center",
          part => part
            .WithPosition("0")
            .WithSettings(
              new CenterSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithPosition("1")
            .WithDisplayName("Naziv")
            .WithDescription("Naziv zatvorenog distribucijskog sustava")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              }))
        .WithPart("AliasPart",
          part => part
            .WithPosition("2")
            .WithDisplayName("Alias")
            .WithDescription("Alias zatvorenog distribucijskog sustava")
            .WithSettings(
              new AliasPartSettings
              {
                Options = AliasPartOptions.Editable,
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithPosition("3")
            .WithDisplayName("Ruta")
            .WithDescription(
              "Automatski generirana ruta zatvorenog distribucijskog sustava")
            .WithSettings(
              new AutoroutePartSettings
              {
                ManageContainedItemRoutes = true,
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              }))
        .WithPart("PersonPart",
          part => part
            .WithPosition("4")
            .WithDisplayName("Zastupna osoba")
            .WithDescription(
              "Poslovni i kontakt podaci zastupne osobe " +
              "zatvorenog distribucijskog sustava")
            .WithSettings(
              new PersonPartSettings
              {
              }))
        .WithPart("BagPart",
          part => part
            .WithPosition("5")
            .WithDisplayName("OMM")
            .WithDescription("Primarna obračunska mjerna mjesta")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Site"
                }
              }))
        .WithPart("ListPart",
          part => part
            .WithPosition("6")
            .WithDisplayName("Članovi")
            .WithDescription("Članovi zatvorenog distribucijskog sustava")
            .WithSettings(
              new ListPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Member"
                }
              })));

  public static void AlterCenterPart(
      this IContentDefinitionManager contentDefinitionManager) =>
    contentDefinitionManager.AlterPartDefinition("Center",
      part => part
        .WithField("User",
          field => field
            .OfType("UserPickerField")
            .WithDisplayName("Korisnik")
            .WithDescription("Korisnički račun zastupnika")
            .WithPosition("0")
            .WithSettings(
              new UserPickerFieldSettings
              {
                Required = true,
                DisplayAllUsers = true,
                Multiple = false,
              })));
}
