using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceiptItem {
  public static void AlterReceiptItemType(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition("ReceiptItem",
          type => type.DisplayedAs("Stavka računa")
                      .Creatable()
                      .Listable()
                      .Securable()
                      .WithPart("ReceiptItem", part => part.WithPosition("0")));

  public static void AlterReceiptItemPart(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterPartDefinition("ReceiptItem",
          part =>
              part.Attachable()
                  .WithDisplayName("Stavka računa")
                  .WithDescription(
                      "Usluga pružana korisnicima ZDS-a i njen mjesečni iznos.")
                  .WithField("OrdinalNumber",
                      field => field.OfType("TextField")
                                   .WithDisplayName("Redni broj")
                                   .WithPosition("0"))
                  .WithField("Name", field => field.OfType("TextField")
                                                  .WithDisplayName("Name")
                                                  .WithPosition("1"))
                  .WithField(
                      "Unit", field => field.OfType("TextField")
                                           .WithDisplayName("Mjerna jedinica")
                                           .WithPosition("2"))
                  .WithField("Amount", field => field.OfType("NumericField")
                                                    .WithDisplayName("Količina")
                                                    .WithPosition("3"))
                  .WithField("Price", field => field.OfType("NumericField")
                                                   .WithDisplayName("Cijena")
                                                   .WithPosition("4"))
                  .WithField("InTotal", field => field.OfType("NumericField")
                                                     .WithDisplayName("Ukupno")
                                                     .WithPosition("5")));
}
