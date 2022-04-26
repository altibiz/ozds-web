using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterContact
{
  public static void AlterContactPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Contact",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Kontakt")
        .WithDescription("Kontakt podaci osobe")
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
        .WithField("Phone",
          field => field
            .OfType("TextField")
            .WithDisplayName("Broj telefona")
            .WithEditor("Tel")
            .WithPosition("5")
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
            .WithPosition("6")
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
            .WithPosition("7")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "445pqtg9kka9hzxgdj30x9qq4g",
                Unique = true,
                Required = true
              })));
}
