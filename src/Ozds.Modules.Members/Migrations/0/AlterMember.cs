using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Lists.Models;
using OrchardCore.Taxonomies.Settings;
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
              .WithPart("Member", part => part.WithPosition("0").WithSettings(
                                      new MemberSettings()))
              .WithPart("AliasPart", part => part.WithPosition("2"))
              .WithPart("TitlePart",
                  part => part.WithPosition("1").WithSettings(
                      new TitlePartSettings
                      {
                        Options = TitlePartOptions.GeneratedDisabled,
                        Pattern =
                            "{{ ContentItem.Content.PersonPart.Name.Text }} {{ ContentItem.Content.PersonPart.Surname.Text }}",
                      }))
              .WithPart(
                  "ListPart", part => part.WithPosition("3").WithSettings(
                                  new ListPartSettings
                                  {
                                    PageSize = 10,
                                    ContainedContentTypes = new[] { "Company" },
                                  })));

  public static void AlterMemberPart(this IContentDefinitionManager content) =>
      content.AlterPartDefinition("Member",
          part => part

                      .WithField("DateOfBirth",
                          field => field.OfType("DateField")
                                       .WithDisplayName("Datum rođenja")
                                       .WithPosition("3"))
                      .WithField("User",
                          field => field.OfType("UserPickerField")
                                       .WithDisplayName("User")
                                       .WithPosition("11")
                                       .WithSettings(
                                           new UserPickerFieldSettings
                                           {
                                             DisplayAllUsers = true,
                                             DisplayedRoles = new string[] { },
                                           }))
                      .WithField("Sex",
                          field => field.OfType("TaxonomyField")
                                       .WithDisplayName("Spol")
                                       .WithEditor("Tags")
                                       .WithDisplayMode("Tags")
                                       .WithPosition("8")
                                       .WithSettings(new TaxonomyFieldSettings
                                       {
                                         TaxonomyContentItemId =
                                             "4xgh8bvawx8h2rvyg7vds118w4",
                                         Unique = true,
                                       }))
                      .WithField("AdminNotes",
                          field => field.OfType("TextField")
                                       .WithDisplayName("Admin bilješke")
                                       .WithEditor("TextArea")
                                       .WithPosition("12")));
}
