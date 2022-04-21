using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.M0;

using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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

    Content.AlterPersonPart();
    Schema.CreatePersonPartMapTable();
    Schema.CreatePersonPartMapIndex();

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
    Schema.CreateCalculationMapTable();

    Content.AlterCalculationItemPart();
    Content.AlterCalculationItemType();

    Content.AlterMemberPart();
    Content.AlterMemberType();
    Schema.CreateMemberMapTable();

    Content.AlterCenterType();
    Content.AlterCenterPart();
    Schema.CreateCenterMapTable();

    Recipe.ExecuteUserLandingPageMenu(this);

    if (Env.IsDevelopment())
    {
      Session.SaveUserTestOwner();
      Session.SaveUserTestMember();
    }

    if (Env.IsDevelopment())
    {
      Recipe.ExecuteTestCenter(this);
      Recipe.ExecuteTestMember(this);
      Recipe.ExecuteTestReceipt(this);
    }

    return 1;
  }

  public Migrations(
      IHostEnvironment env,
      ILogger<Migrations> logger,
      IConfiguration conf,
      IRecipeMigrator recipe,
      IContentDefinitionManager content,
      ISession session)
  {
    Env = env;
    Logger = logger;
    Conf = conf;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }
  private IConfiguration Conf { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
