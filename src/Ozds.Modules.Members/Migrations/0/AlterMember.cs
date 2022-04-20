using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;
using Ozds.Modules.Members.Core;

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
            .WithPosition("1")
            .WithDisplayName("Naziv")
            .WithDisplayName("Naziv clana")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.GeneratedDisabled,
                // TODO: check
                Pattern =
                @"""
                  {{%- assign person = ContentItem.Content.PersonPart -%}}
                  {{%- assign isLegal = person.Legal.Value -%}}
                  {{%- assign name = person.Name.Text -%}}
                  {{%- if isLegal -%}}
                    {{- name -}}
                  {{%- else -%}}
                    {{%- assign middleName = person.MiddleName.Text -%}}
                    {{%- assign surname = person.Surname.Text -%}}
                    {{%- if middleName -%}}
                      {{- name }} {{ middleName }} {{ surname -}}
                    {{%- else -%}}
                      {{- name }} {{ surname -}}
                    {{%- endif -%}}
                  {{%- endif -%}}
                """,
              }))
        .WithPart("PersonPart",
          part => part
            .WithPosition("2")
            .WithDisplayName("Poslovni i kontakt podaci")
            .WithDescription("Poslovni i kontakt podaci clana")
            .WithSettings(
              new PersonPartSettings
              {
              }))
        .WithPart("BagPart",
          part => part
            .WithPosition("3")
            .WithDisplayName("OMM")
            .WithDescription("Sekundarna obračunska mjerna mjesta")
            .WithSettings(
              new BagPartSettings
              {
                ContainedContentTypes = new[]
                {
                  "Site"
                }
              })));

  public static void AlterMemberPart(this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Member",
      part => part
        .WithField("User",
          field => field
            .OfType("UserPickerField")
            .WithDisplayName("Korisnik")
            .WithDescription("Korisnicki racun clana")
            .WithPosition("0")
            .WithSettings(
              new UserPickerFieldSettings
              {
                Required = true,
                DisplayAllUsers = true,
                Multiple = false,
              })));
}
