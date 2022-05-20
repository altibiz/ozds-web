using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Ozds.M0;

public static partial class AlterPerson
{
  public static void AlterPersonPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Person",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Osoba")
        .WithField("Name",
          field => field
            .OfType("TextField")
            .WithDisplayName("Naziv")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Oib",
          field => field
            .OfType("TextField")
            .WithDisplayName("OIB")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Address",
          field => field
            .OfType("TextField")
            .WithDisplayName("Adresa")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("City",
          field => field
            .OfType("TextField")
            .WithDisplayName("Grad/Općina")
            .WithPosition("4")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("PostalCode",
          field => field
            .OfType("TextField")
            .WithDisplayName("Poštanski broj")
            .WithPosition("5")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Contact",
          field => field
            .OfType("TextField")
            .WithDisplayName("Kontakt")
            .WithPosition("6")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              })));
}
