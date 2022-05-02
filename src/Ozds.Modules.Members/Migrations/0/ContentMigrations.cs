using OrchardCore.ContentManagement.Metadata;

namespace Ozds.Modules.Members.M0;

public static class ContentMigrations
{
  public static IContentDefinitionManager AlterContent(
      this IContentDefinitionManager @this)
  {
    @this.AlterTagPart();
    @this.AlterTagType();
    @this.AlterTariffElementPart();
    @this.AlterTariffElementType();

    @this.AlterSitePart();
    @this.AlterSecondarySiteType();
    @this.AlterSecondarySitePart();

    @this.AlterExpenditureType();
    @this.AlterExpenditurePart();
    @this.AlterExpenditureItemType();
    @this.AlterExpenditureItemPart();
    @this.AlterCalculationPart();
    @this.AlterCalculationType();
    @this.AlterReceiptPart();
    @this.AlterReceiptType();
    @this.AlterReceiptItemPart();
    @this.AlterReceiptItemType();

    @this.AlterPersonType();
    @this.AlterPersonPart();
    @this.AlterConsumerType();
    @this.AlterConsumerType();
    @this.AlterCenterType();
    @this.AlterCenterPart();

    return @this;
  }
}
