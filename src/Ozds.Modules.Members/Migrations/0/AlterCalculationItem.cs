using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterCalculationItem {
  public static void AlterCalculationItemType(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition("CalculationItem",
          type => type.DisplayedAs("Stavka obračuna")
                      .Creatable()
                      .Listable()
                      .Draftable()
                      .Versionable()
                      .Securable()
                      .WithPart("Item", part => part.WithPosition("0")));

  public static void AlterCalculationItemPart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition("CalculationItem",
          part =>
              part.Attachable()
                  .WithDisplayName("Stavka obračuna")
                  .WithDescription("Tarifna stavka mjesečnog obračuna.")
                  .WithField("Tariff", field => field.OfType("TextField")
                                                    .WithDisplayName("Tarifa")
                                                    .WithPosition("0"))
                  .WithField(
                      "ValueFrom", field => field.OfType("NumericField")
                                                .WithDisplayName("Stanje od")
                                                .WithPosition("1"))
                  .WithField(
                      "ValueTo", field => field.OfType("NumericField")
                                              .WithDisplayName("Stanje do")
                                              .WithPosition("2"))
                  .WithField("Status", field => field.OfType("TextField")
                                                    .WithDisplayName("Status")
                                                    .WithPosition("3"))
                  .WithField(
                      "Constant", field => field.OfType("NumericField")
                                               .WithDisplayName("Konstanta")
                                               .WithPosition("4"))
                  .WithField(
                      "Consumption", field => field.OfType("NumericField")
                                                  .WithDisplayName("Potrošak")
                                                  .WithPosition("5"))
                  .WithField("UnitPrice",
                      field => field.OfType("NumericField")
                                   .WithDisplayName("Jedinična cijena")
                                   .WithPosition("6"))
                  .WithField("Amount", field => field.OfType("NumericField")
                                                    .WithDisplayName("Iznos")
                                                    .WithPosition("7")));
}
