using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;

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
        .WithPart("Member",
          part => part
            .WithPosition("0")
            .WithSettings(
              new MemberSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithDisplayName("Naziv člana")
            .WithPosition("1")
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
        .WithPart("Person",
          part => part
            .WithDisplayName("Poslovni i kontakt podaci")
            .WithDescription("Poslovni i kontakt podaci člana")
            .WithPosition("2")
            .WithSettings(
              new PersonSettings
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
            .WithPosition("0")
            .WithSettings(
              new UserPickerFieldSettings
              {
                Required = true,
                DisplayAllUsers = true,
                Multiple = false,
              }))
        .WithField("SecondarySites",
          part => part
            .OfType("ContentPickerField")
            .WithDisplayName("Sekundarna obračunska mjerna mjesta")
            .WithDescription(
              "Sekundarna obračunska mjerna mjesta člana")
            .WithPosition("1")
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
