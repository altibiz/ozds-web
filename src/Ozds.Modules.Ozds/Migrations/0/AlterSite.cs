using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Taxonomies.Settings;
using Etch.OrchardCore.Fields.Dictionary.Settings;

namespace Ozds.Modules.Ozds.M0;

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
        .WithField("SourceDeviceId",
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
        .WithField("MeasurementIntervalInSeconds",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Interval mjeranja (s)")
            .WithDescription(
              "Količina vremena između uzastopnih mjerenja uređaja")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
              }))
        .WithField("ExtractionStart",
          field => field
            .OfType("DateTimeField")
            .WithDisplayName("Početak mjerenja")
            .WithDescription("Trenutak u kojem je uređaj počeo mjeriti")
            .WithPosition("5")
            .WithSettings(
              new DateTimeFieldSettings
              {
                Required = true,
              }))
        .WithField("ExtractionOffsetInSeconds",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Odstupanje preuzimanja mjerenja (s)")
            .WithDescription(
              "Količina vremena u sekundama koja je potrebna " +
              "da bi mjerenja uređaja bila raspoloživa na izvoru")
            .WithPosition("6")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
              }))
        .WithField("ExtractionTimeoutInSeconds",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Odstupanje preuzimanja nedostataka (s)")
            .WithDescription(
              "Količina vremena u sekundama nakon koje će nastupiti " +
              "ponovni pokušaj preuzimanja mjerenja u iznimnim situacijama")
            .WithPosition("7")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
              }))
        .WithField("ExtractionRetries",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Količina ponovnih pokušaja preuzimanja")
            .WithDescription(
              "Koliko će se puta mjerenja ponovno pukušati preuzeti " +
              "u iznimnim situacijama")
            .WithPosition("8")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
              }))
        .WithField("ValidationIntervalInSeconds",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Interval validacije mjerenja (s)")
            .WithDescription(
              "Mjerenja će se provjeravati za validnost svaki interval")
            .WithPosition("9")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
              }))
        .WithField("Status",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Status")
            .WithPosition("10")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "47p9e5rkms3m012qv3z2t26jcg",
                Required = true,
                Unique = true,
              })));
}