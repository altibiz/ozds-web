using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterLocation
{
  public static void AlterLocationPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Location",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Lokacija")
        .WithDescription("Adresa, grad/općina, poštanski broj, županija")
        .WithField("Address",
          field => field
            .OfType("TextField")
            .WithDisplayName("Adresa")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("City",
          field => field
            .OfType("TextField")
            .WithDisplayName("Grad/Općina")
            .WithPosition("3")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true,
              }))
        .WithField("PostalCode",
          field => field
            .OfType("TextField")
            .WithDisplayName("Poštanski broj")
            .WithPosition("4")
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
            .WithPosition("5")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4d0dew9ar7h9nsbpcs7jg2egwe",
                Unique = true,
                Required = true,
              })));
}
