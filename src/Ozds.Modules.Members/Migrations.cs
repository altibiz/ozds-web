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

    Recipe.ExecuteCountyTaxonomy(this);
    Recipe.ExecuteCurrencyTaxonomy(this);
    Recipe.ExecuteArticleTaxonomy(this);
    Recipe.ExecutePhaseTaxonomy(this);
    Recipe.ExecuteMeasurementUnitTaxonomy(this);
    Recipe.ExecuteCalculationItemStatusTaxonomy(this);
    Recipe.ExecuteTariffTaxonomy(this);
    Recipe.ExecuteSiteTypeTaxonomy(this);
    Recipe.ExecutePersonTypeTaxonomy(this);

    Content.AlterAdminPageType();

    Content.AlterTagPart();
    Content.AlterTagType();

    Content.AlterImagePart();
    Content.AlterImageType();

    Content.AlterPersonType();
    Content.AlterPersonPart();
    Schema.CreatePersonMapTable();
    Schema.CreatePersonMapIndex();

    Content.AlterSiteType();
    Content.AlterSitePart();
    Schema.CreateSiteMapTable();

    Content.AlterReceiptPart();
    Content.AlterReceiptType();
    Schema.CreateReceiptMapTable();

    Content.AlterReceiptItemPart();
    Content.AlterReceiptItemType();

    Content.AlterCalculationPart();
    Content.AlterCalculationType();
    Content.AlterCalculationItemPart();
    Content.AlterCalculationItemType();
    Schema.CreateCalculationMapTable();

    Content.AlterMemberPart();
    Content.AlterMemberType();
    Schema.CreateMemberMapTable();

    Content.AlterCenterType();
    Content.AlterCenterPart();
    Schema.CreateCenterMapTable();

    Content.AlterCataloguePart();
    Content.AlterCatalogueType();
    Content.AlterCatalogueItemPart();
    Content.AlterCatalogueItemType();
    Schema.CreateCatalogueMapTable();

    Content.AlterContractPart();
    Content.AlterContractType();
    Schema.CreateContractMapTable();

    Recipe.ExecuteUserLandingPageMenu(this);

    if (Env.IsDevelopment())
    {
      Session.SaveUserTestOwner();
      Session.SaveUserTestMember();
    }

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
