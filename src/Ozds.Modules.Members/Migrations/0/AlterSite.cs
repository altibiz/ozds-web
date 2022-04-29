using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterSite
{
  public static void AlterSitePart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("Site",
      part => part
        .Attachable()
        .Reusable()
        .WithDisplayName("Obračunsko mjerno mjesto")
        .WithDescription("Obračunsko mjerno mjesto")
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
        .WithField("Status",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Status")
            .WithPosition("2")
            .WithSettings(
              new BooleanFieldSettings
              {
                DefaultValue = false
              })));
}
