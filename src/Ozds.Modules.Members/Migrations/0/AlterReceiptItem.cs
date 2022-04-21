using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.ContentFields.Settings;

namespace Ozds.Modules.Members.M0;

public static partial class AlterReceiptItem
{
  public static void AlterReceiptItemType(
      this IContentDefinitionManager content) =>
    content.AlterTypeDefinition("ReceiptItem",
      type => type
        .DisplayedAs("Stavka računa")
        .Creatable()
        .Listable()
        .Securable()
        .WithPart("ReceiptItem",
          part => part
            .WithPosition("0")
            .WithSettings(
              new ReceiptItemSettings
              {
              })));

  public static void AlterReceiptItemPart(
      this IContentDefinitionManager content) =>
    content.AlterPartDefinition("ReceiptItem",
      part => part
        .Attachable()
        .WithDisplayName("Stavka računa")
        .WithDescription(
          "Usluga pružana korisnicima ZDS-a i njen mjesečni iznos.")
        .WithField("OrdinalNumber",
          field => field
            .OfType("TextField")
            .WithDisplayName("Redni broj")
            .WithPosition("0")
            .WithSettings(
              new TextFieldSettings
              {
                Required = true
              }))
        .WithField("Article",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Artikl")
            .WithPosition("1")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4dyah7e7srewr46d894deh28n4",
                Unique = true,
                Required = true
              }))
        .WithField("Unit",
          field => field
            .OfType("TaxonomyField")
            .WithDisplayName("Mjerna jedinica")
            .WithPosition("2")
            .WithSettings(
              new TaxonomyFieldSettings
              {
                TaxonomyContentItemId = "4cqf2eeqqwadb4xechw3tbbsn0",
                Unique = true
              }))
        .WithField("Amount",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Količina")
            .WithPosition("3")
            .WithSettings(
              new NumericFieldSettings
              {
                Minimum = 0
              }))
        .WithField("Price",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Cijena")
            .WithPosition("4")
            .WithSettings(
              new NumericFieldSettings
              {
                Minimum = 0
              }))
        .WithField("InTotal",
          field => field
            .OfType("NumericField")
            .WithDisplayName("Ukupno")
            .WithPosition("5")
            .WithSettings(
              new NumericFieldSettings
              {
                Required = true,
                Minimum = 0,
              })));
}
