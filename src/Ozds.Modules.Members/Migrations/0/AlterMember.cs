using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using Ozds.Modules.Members.Persons;
using Ozds.Modules.Members.Core;

namespace Ozds.Modules.Members.M0;

public static partial class Migration0AlterMember
{
  public static void AlterMemberType(this IContentDefinitionManager
          content) => content.AlterTypeDefinition("Member",
      type =>
          type.DisplayedAs("Član")
              .Creatable()
              .Listable()
              .Securable()
              .WithPart("PersonPart",
                  part => part.WithPosition("0").WithSettings(
                      new PersonPartSettings { Type = PersonType.Natural }))
              .WithPart("Member", part => part.WithPosition("1").WithSettings(
                                      new MemberSettings { }))
              .WithPart("AliasPart", part => part.WithPosition("2"))
              .WithPart("TitlePart",
                  part => part.WithPosition("3").WithSettings(
                      new TitlePartSettings
                      {
                        Options = TitlePartOptions.GeneratedDisabled,
                        Pattern =
                            @"{{ ContentItem.Content.PersonPart.Title.Text }}",
                      })));

  public static void AlterMemberPart(this IContentDefinitionManager content) =>
      content.AlterPartDefinition(
          "Member", part => part.WithField("User",
                        field => field.OfType("UserPickerField")
                                     .WithDisplayName("Korisnik")
                                     .WithPosition("0")
                                     .WithSettings(new UserPickerFieldSettings
                                     {
                                       DisplayAllUsers = true,
                                       DisplayedRoles = new string[] { },
                                     })));
}
