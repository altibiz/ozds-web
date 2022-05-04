using OrchardCore.ContentManagement.Metadata;

namespace Ozds.Modules.Members.M0;

public static class ContentMigrations
{
  public static IContentDefinitionManager AlterContent(
      this IContentDefinitionManager content)
  {
    content.AlterTagPart();
    content.AlterTagType();
    content.AlterTariffElementPart();
    content.AlterTariffElementType();

    content.AlterSitePart();
    content.AlterSecondarySiteType();
    content.AlterSecondarySitePart();

    content.AlterCatalogueItemPart();
    content.AlterCatalogueItemType();
    content.AlterCataloguePart();
    content.AlterCatalogueType();

    content.AlterExpenditureType();
    content.AlterExpenditurePart();
    content.AlterExpenditureItemType();
    content.AlterExpenditureItemPart();
    content.AlterCalculationPart();
    content.AlterCalculationType();
    content.AlterReceiptPart();
    content.AlterReceiptType();
    content.AlterReceiptItemPart();
    content.AlterReceiptItemType();

    content.AlterPersonPart();
    content.AlterConsumerType();
    content.AlterConsumerPart();
    content.AlterCenterType();
    content.AlterCenterPart();

    return content;
  }
}
