using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.M0;

namespace Ozds.Modules.Members;

public sealed class Migrations : DataMigration
{
  public Migrations(IHostEnvironment env, ILogger<Migrations> logger,
      IRecipeMigrator recipe, IContentDefinitionManager content,
      ISession session)
  {
    Env = env;
    Logger = logger;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  public int Create()
  {
    Recipe.ExecuteAuthSettings(this);

    Content.AlterAdminPageType();

    Content.AlterTagPart();
    Content.AlterTagType();

    Content.AlterImagePart();
    Content.AlterImageType();

    Content.AlterReceiptPart();
    Content.AlterReceiptType();

    Content.AlterReceiptItemPart();
    Content.AlterReceiptItemType();

    Content.AlterCalculationPart();
    Content.AlterCalculationType();

    Content.AlterCalculationItemPart();
    Content.AlterCalculationItemType();

    Content.AlterMemberPart();
    Content.AlterMemberType();

    Content.AlterPersonPart();
    Schema.CreatePersonPartMapTable();
    Schema.CreatePersonPartMapIndex();

    Content.AlterSiteType();
    Content.AlterSitePart();

    Content.AlterCenterType();
    Content.AlterCenterPart();

    Recipe.ExecuteUserLandingPageMenu(this);

    Recipe.ExecuteArticleTaxonomy(this);
    Recipe.ExecuteCalculationItemStatusTaxonomy(this);
    Recipe.ExecuteCountyTaxonomy(this);
    Recipe.ExecuteCurrencyTaxonomy(this);
    Recipe.ExecuteMeasurementUnitTaxonomy(this);
    Recipe.ExecutePhaseTaxonomy(this);
    Recipe.ExecuteSiteTypeTaxonomy(this);
    Recipe.ExecuteTariffTaxonomy(this);

    return 1;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
