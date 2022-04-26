using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterMember
{
  public static void AlterMemberType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Member",
      type => type
        .DisplayedAs("Član")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithDisplayName("Naziv člana")
            .WithPosition("0")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
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
        .WithPart("Member",
          part => part
            .WithPosition("1")
            .WithDisplayName("Član")
            .WithSettings(
              new MemberSettings
              {
              }))
        .WithPart("Person",
          part => part
            .WithDisplayName("Osoba")
            .WithDescription("Poslovni i kontakt podaci člana")
            .WithPosition("2")
            .WithSettings(
              new PersonSettings
              {
              }))
        .WithPart("Contact",
          part => part
            .WithDisplayName("Kontakt")
            .WithDescription("Podaci za kontaktiranje člana")
            .WithPosition("3")
            .WithSettings(
              new ContactSettings
              {
              }))
        .WithPart("Location",
          part => part
            .WithDisplayName("Adresa dostave")
            .WithDescription("Adresa dostave dokumenata člana")
            .WithPosition("4")
            .WithSettings(
              new LocationSettings
              {
              })));

  public static void AlterMemberPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Member",
      part => part
        .WithField("User",
          field => field
            .OfType("UserPickerField")
            .WithDisplayName("Korisnik")
            .WithDescription("Korisnički račun člana")
            .WithPosition("1")
            .WithSettings(
              new UserPickerFieldSettings
              {
                Required = true,
                DisplayAllUsers = true,
                Multiple = false,
              }))
        .WithField("Activity",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Djelatnost")
            .WithPosition("2")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                Required = true,
                Unique = true,
                TaxonomyContentItemId = "4fm60kwhbgryxyasm37nrdk5de"
              }))
        .WithField("ConnectionContract",
          field => field
            .OfType("ContentPickerFieldSettings")
            .WithDisplayName("Ugovor o priključenju")
            .WithPosition("3")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Multiple = false,
                Required = true,
                DisplayedContentTypes =
                new[]
                {
                  "Contract"
                }
              }))
        .WithField("UsageContract",
          field => field
            .OfType("ContentPickerFieldSettings")
            .WithDisplayName("Ugovor o korištenju i opskrbi")
            .WithPosition("4")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Multiple = false,
                Required = true,
                DisplayedContentTypes =
                new[]
                {
                  "Contract"
                }
              }))
        .WithField("Note",
          field => field
            .OfType("TextField")
            .WithEditor("Textarea")
            .WithDisplayName("Napomena")
            .WithPosition("5")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("SecondarySites",
          part => part
            .OfType("ContentPickerField")
            .WithDisplayName("Sekundarna obračunska mjerna mjesta")
            .WithDescription(
              "Sekundarna obračunska mjerna mjesta člana")
            .WithPosition("6")
            .WithSettings(
              new ContentPickerFieldSettings
              {
                Required = true,
                Multiple = true,
                DisplayedContentTypes = new[]
                {
                  "SecondarySite"
                }
              })));
}
