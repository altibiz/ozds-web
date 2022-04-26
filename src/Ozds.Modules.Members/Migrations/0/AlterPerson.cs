using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterPerson
{
  public static void AlterPersonType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Person",
      type => type
        .DisplayedAs("Osoba")
        .Creatable()
        .Listable()
        .Securable()
        .Draftable()
        .Versionable()
        .WithPart("Person",
          part => part
          .WithPosition("2")
          .WithSettings(
            new PersonSettings
            {
            })));

  public static void AlterPersonPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Person",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Osoba")
        .WithDescription("Poslovni i kontakt podaci osobe.")
        .WithField("Name",
          field => field
            .OfType("TextField")
            .WithDisplayName("Ime")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("MiddleName",
          field => field
            .OfType("TextField")
            .WithDisplayName("Srednje ime")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("Surname",
          field => field
            .OfType("TextField")
            .WithDisplayName("Prezime")
            .WithPosition("4")
            .WithSettings(
              new TextFieldSettings
              {
                Required = false,
              }))
        .WithField("Oib",
          field => field
            .OfType("TextField")
            .WithDisplayName("OIB")
            .WithPosition("5")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("Address",
          field => field
            .OfType("TextField")
            .WithDisplayName("Adresa")
            .WithPosition("6")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("City",
          field => field
            .OfType("TextField")
            .WithDisplayName("Grad/Općina")
            .WithPosition("7")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("PostalCode",
          field => field
            .OfType("TextField")
            .WithDisplayName("Poštanski broj")
            .WithPosition("8")
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
            .WithPosition("9")
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
            .WithPosition("10")
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
            .WithPosition("11")
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
            .WithPosition("12")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "445pqtg9kka9hzxgdj30x9qq4g",
                Unique = true,
                Required = true
              })));
}
