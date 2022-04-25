using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Spatial.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterSite
{
  public static void AlterSitePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Site",
      part => part
        .Attachable()
        .Reusable()
        .WithField("Source",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Izvor mjerenja uređaja")
            .WithPosition("0")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                Required = true,
                Unique = true,
                TaxonomyContentItemId = "4k4556m076b1vvsmmqjccbjwn5"
              }))
        .WithField("DeviceId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Identifikator uređaja")
            .WithPosition("1")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Coefficient",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Koeficijent")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Phase",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Faza")
            .WithPosition("4")
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
            .WithPosition("5")
            .WithSettings(
              new GeoPointFieldSettings
              {
                Required = true
              }))
        .WithField("Active",
          field => field
            .OfType("BooleanField")
            .WithDisplayName("Aktivno")
            .WithPosition("6")
            .WithSettings(
              new BooleanFieldSettings
              {
                DefaultValue = false
              })));
}
