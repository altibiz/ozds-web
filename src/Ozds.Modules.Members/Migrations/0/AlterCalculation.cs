using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCalculation
{
  public static void AlterCalculationType(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition("Calculation",
          type =>
              type.DisplayedAs("Obračun")
                  .Creatable()
                  .Listable()
                  .Draftable()
                  .Securable()
                  .WithPart("Calculation", part => part.WithPosition("0"))
                  .WithPart("Items",
                      part => part.WithDisplayName("Stavke")
                                  .WithDescription("Stavke mjesečnog obračuna")
                                  .WithSettings(new BagPartSettings
                                  {
                                    ContainedContentTypes = new[] { "Item" },
                                  })));

  public static void AlterCalculationPart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition("Calculation",
          part =>
              part.WithField("DeviceNumber",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Broj brojila"))
                  .WithField("DateFrom",
                      field =>
                          field.OfType("DateField").WithDisplayName("Datum od"))
                  .WithField("DateTo",
                      field =>
                          field.OfType("DateField").WithDisplayName("Datum do"))
                  .WithField("MeasurementServiceFee",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Naknada za mjernu uslugu"))
                  .WithField(
                      "InTotal", field => field.OfType("NumericField")
                                              .WithDisplayName("Ukupno")));
}
