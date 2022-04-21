using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterPerson
{
  public static void AlterPersonPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("PersonPart",
      part => part
        .Attachable()
        .WithDisplayName("Osoba")
        .WithDescription("Poslovni i kontakt podaci osobe.")
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
        .WithField("MiddleName",
          field => field
            .OfType("TextField")
            .WithDisplayName("Srednje ime")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("Surname",
          field => field
            .OfType("TextField")
            .WithDisplayName("Prezime")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("Oib",
          field => field
            .OfType("TextField")
            .WithDisplayName("OIB")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Address",
          field => field
            .OfType("TextField")
            .WithDisplayName("Adresa")
            .WithPosition("4")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("City",
          field => field
            .OfType("TextField")
            .WithDisplayName("Grad/Općina")
            .WithPosition("5")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("County",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Županija")
            .WithEditor("Tags")
            .WithDisplayMode("Tags")
            .WithPosition("7")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4d0dew9ar7h9nsbpcs7jg2egwe",
                Unique = true,
                Required = true,
              }))
        .WithField("Phone",
          field => field
            .OfType("TextField")
            .WithDisplayName("Broj telefona")
            .WithEditor("Tel")
            .WithPosition("6")
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
            .WithPosition("7")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Type",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tip")
            .WithDescription("Tip osobe")
            .WithPosition("8")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "445pqtg9kka9hzxgdj30x9qq4g",
                Unique = true,
                Required = true
              })));
}
