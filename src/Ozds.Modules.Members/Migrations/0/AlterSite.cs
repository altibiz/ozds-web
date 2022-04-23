using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;
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
            .WithSettings(
              new SiteSettings
              {
              }))
        .WithPart("TitlePart",
          part => part
            .WithDisplayName("Naziv")
            .WithSettings(
              new TitlePartSettings
              {
                RenderTitle = true,
                Options = TitlePartOptions.EditableRequired,
              })));

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
            .WithSettings(
              new TaxonomyFieldSettings
              {
                Required = true,
                Unique = true,
                TaxonomyContentItemId = ""
              }))
        .WithField("DeviceId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Identifikator uređaja")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Type",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Tip")
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
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Phase",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Faza")
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
            .WithSettings(
              new GeoPointFieldSettings
              {
                Required = true
              }))
        .WithField("Active",
          field => field
            .OfType("BooleanField")
            .WithDisplayName("Aktivno")
            .WithSettings(
              new BooleanFieldSettings
              {
                DefaultValue = false
              })));
}
