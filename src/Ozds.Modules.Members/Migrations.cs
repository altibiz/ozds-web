using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.M0;

namespace Ozds.Modules.Members;

public sealed class Migrations : DataMigration
{
  public int Create()
  {
    Recipe.ExecuteAuthSettings(this);

    Recipe.ExecuteTariffElementTaxonomy(this);
    if (Env.IsDevelopment())
    {
      Recipe.ExecuteTestSiteMeasurementSourceTaxonomy(this);
    }
    else
    {
      Recipe.ExecuteSiteMeasurementSourceTaxonomy(this);
    }

    Content.AlterTagPart();
    Content.AlterTagType();
    Content.AlterTariffElementPart();
    Content.AlterTariffElementType();

    Content.AlterSitePart();
    Content.AlterSecondarySiteType();
    Content.AlterSecondarySitePart();

    Content.AlterExpenditureType();
    Content.AlterExpenditurePart();
    Content.AlterExpenditureItemType();
    Content.AlterExpenditureItemPart();
    Content.AlterCalculationPart();
    Content.AlterCalculationType();
    Content.AlterReceiptPart();
    Content.AlterReceiptType();
    Content.AlterReceiptItemPart();
    Content.AlterReceiptItemType();

    Content.AlterPersonType();
    Content.AlterPersonPart();
    Content.AlterConsumerType();
    Content.AlterConsumerType();
    Content.AlterCenterType();
    Content.AlterCenterPart();

    if (Env.IsDevelopment())
    {
      Recipe.ExecuteTestCenterSite(this);
      Recipe.ExecuteTestMemberSite(this);
      Recipe.ExecuteTestCenter(this);
      Recipe.ExecuteTestMember(this);
      Recipe.ExecuteTestReceipt(this);
    }

    return 1;
  }

  public Migrations(
      IHostEnvironment env,
      IRecipeMigrator recipe,
      IContentDefinitionManager content,
      ISession session)
  {
    Env = env;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  private IHostEnvironment Env { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
