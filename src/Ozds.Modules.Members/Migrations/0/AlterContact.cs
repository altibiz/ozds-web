using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterContact
{
  public static void AlterContactType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Contact",
      type => type
        .DisplayedAs("Kontakt")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("Contact",
          part => part
          .WithPosition("0")
          .WithSettings(
            new ContactSettings
            {
            })));

  public static void AlterContactPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Contact",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Kontakt")
        .WithField("Name",
          field => field
            .OfType("TextField")
            .WithDisplayName("Ime")
            .WithPosition("0")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Phone",
          field => field
            .OfType("TextField")
            .WithDisplayName("Broj telefona")
            .WithEditor("Tel")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Email",
          field => field
            .OfType("TextField")
            .WithDisplayName("Email")
            .WithEditor("Email")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Fax",
          field => field
            .OfType("TextField")
            .WithDisplayName("Faks")
            .WithEditor("Tel")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              })));
}
