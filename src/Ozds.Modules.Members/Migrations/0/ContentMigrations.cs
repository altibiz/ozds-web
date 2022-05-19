using OrchardCore.ContentManagement.Metadata;

namespace Ozds.Modules.Members.M0;

public static class ContentMigrations
{
  public static IContentDefinitionManager AlterContent(
      this IContentDefinitionManager content)
  {
    content.AlterTagPart();
    content.AlterTagType();
    content.AlterTariffTagPart();
    content.AlterTariffTagType();

    content.AlterSitePart();
    content.AlterSecondarySiteType();
    content.AlterSecondarySitePart();

    content.AlterCatalogueItemPart();
    content.AlterCatalogueItemType();
    content.AlterCataloguePart();
    content.AlterCatalogueType();

    content.AlterReceiptPart();
    content.AlterReceiptType();

    content.AlterPersonPart();
    content.AlterConsumerType();
    content.AlterConsumerPart();
    content.AlterCenterType();
    content.AlterCenterPart();

    return content;
  }
}
