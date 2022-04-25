using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;
using OrchardCore.ContentFields.Settings;
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
            .WithDisplayName("Naziv")
            .WithDescription("Naziv zatvorenog distribucijskog sustava")
            .WithPosition("1")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
                Pattern =
                @"
                  {%- assign person = ContentItem.Content.Person -%}
                  {%- assign types = person.Type.TermContentItemIds -%}
                  {%- assign isLegal = types contains '43jw9bej0w1tqybrryfm3nek44' -%}
                  {%- assign name = person.Name.Text -%}
                  {%- if isLegal -%}
                    {{- name -}}
                  {%- else -%}
                    {%- assign middleName = person.MiddleName.Text -%}
                    {%- assign surname = person.Surname.Text -%}
                    {%- if middleName -%}
                      {{- name }} {{ middleName }} {{ surname -}}
                    {%- else -%}
                      {{- name }} {{ surname -}}
                    {%- endif -%}
                  {%- endif -%}
                ",
              }))
        .WithPart("AutoroutePart",
          part => part
            .WithDisplayName("Ruta")
            .WithDescription(
              "Automatski generirana ruta zatvorenog distribucijskog sustava")
            .WithPosition("2")
            .WithSettings(
              new AutoroutePartSettings
              {
                ManageContainedItemRoutes = true,
                Pattern = @"{{ ContentItem.Content.TitlePart.Title | slugify }}"
              }))
        .WithPart("Person",
          part => part
            .WithDisplayName("Zastupna osoba")
            .WithDescription(
              "Poslovni i kontakt podaci zastupne osobe " +
              "zatvorenog distribucijskog sustava")
            .WithPosition("3")
            .WithSettings(
              new PersonSettings
              {
              }))
        .WithPart("ListPart",
          part => part
            .WithDisplayName("Članovi")
            .WithDescription("Članovi zatvorenog distribucijskog sustava")
            .WithPosition("4")
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
              }))
        .WithField("PrimarySites",
          part => part
            .OfType("ContentPickerField")
            .WithDisplayName("Primarna obračunska mjerna mjesta")
            .WithDescription(
              "Primarna obračunska mjerna mjesta " +
              "zatvorenog distribucijskog sustava")
            .WithPosition("1")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Required = true,
                Multiple = true,
                DisplayedContentTypes = new[]
                {
                  "PrimarySite"
                }
              })));
}
