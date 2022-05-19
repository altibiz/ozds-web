using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using Etch.OrchardCore.Fields.Dictionary.Settings;

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
        .WithField("SourceData",
          field => field
            .OfType("DictionaryField")
            .WithDisplayName("Izvorišni podaci")
            .WithDescription(
              "Podaci potrebni da bi se preuzimala mjerenja " +
              "sa izvora mjerenja uređaja")
            .WithPosition("3")
            .WithSettings(
              new DictionaryFieldSettings
              {
              }))
        .WithField("MeasurementFrequency",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Frekvencija mjerenja uređaja u sekundama")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true
              }))
        .WithField("Status",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Status")
            .WithPosition("5")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "47p9e5rkms3m012qv3z2t26jcg",
                Required = true,
                Unique = true,
              })));
}
