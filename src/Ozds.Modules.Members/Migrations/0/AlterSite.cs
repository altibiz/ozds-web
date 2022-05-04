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
        .WithDisplayName("Obračunsko mjerno mjesto")
        .WithField("Source",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Izvor mjerenja uređaja")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4k4556m076b1vvsmmqjccbjwn5",
                Required = true,
                Unique = true,
              }))
        .WithField("DeviceId",
          field => field
            .OfType("TextField")
            .WithDisplayName("Identifikator uređaja")
            .WithPosition("2")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Status",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Status")
            .WithPosition("3")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "47p9e5rkms3m012qv3z2t26jcg",
                Required = true,
                Unique = true,
              })));
}
