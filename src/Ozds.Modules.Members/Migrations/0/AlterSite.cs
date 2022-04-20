using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Spatial.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterSite
{
  public static void AlterSiteType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("Site",
      type => type
        .DisplayedAs("Obračunsko mjerno mjesto")
        .Creatable()
        .Listable()
        .Draftable()
        .Securable()
        .WithPart("Site",
          part => part
            .WithPosition("0")
            .WithSettings(
              new SiteSettings
              {
              })));

  public static void AlterSitePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Site",
      part => part
        .Attachable()
        .Reusable()
        .WithField("DeviceId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Šifra uređaja")
            .WithPosition("0")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Type",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tip")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "40afgjpy1kyk7z3p54vgtdhz5a",
                Required = true,
                Unique = true
              }))
        .WithField("Coefficient",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Koeficijent")
            .WithPosition("2")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Phase",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Faza")
            .WithPosition("3")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4p8c2k9qbte1yzcbewfjy5zyxw",
                Required = true,
                Unique = true,
              }))
        .WithField("Geolocation",
          field => field
            .OfType("GeoPointField")
            .WithDisplayName("Geolokacija")
            .WithPosition("4")
            .WithSettings(
              new GeoPointFieldSettings
              {
                Required = true
              }))
        .WithField("Active",
          field => field
            .OfType("BooleanField")
            .WithDisplayName("Aktivno")
            .WithPosition("5")
            .WithSettings(
              new BooleanFieldSettings
              {
                DefaultValue = false
              }))
        // TODO: as SiteSettings field
        // NOTE: Center => Primary Sites
        // NOTE: Member => Secondary Sites
        .WithField("Primary",
          field => field
            .OfType("BooleanField")
            .WithDisplayName("Primarno")
            .WithPosition("6"))
            .WithSettings(
              new BooleanFieldSettings
              {
                DefaultValue = false
              }));
}
