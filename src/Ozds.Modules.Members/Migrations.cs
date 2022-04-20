using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using OrchardCore.Users.Services;
using Ozds.Modules.Members.M0;

using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Ozds.Modules.Members;

public sealed class Migrations : DataMigration
{
  public int Create()
  {
    Recipe.ExecuteAuthSettings(this);

    Content.AlterAdminPageType();

    Content.AlterTagPart();
    Content.AlterTagType();

    Content.AlterImagePart();
    Content.AlterImageType();

    Content.AlterPersonPart();
    Schema.CreatePersonPartMapTable();
    Schema.CreatePersonPartMapIndex();

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

    if (Env.IsDevelopment())
    {
      // NOTE: this one breaks the schema transaction for some reason
      // Users.AddTestUsers(Logger, Conf);

      // NOTE: this one is similar to Users.AddTestUsers, but does this
      // NOTE: through a recipe
      // Recipe.ExecuteCommandCreateTestUsers(this);

      // NOTE: these add users directly from JSON
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
      ISession session,
      IUserService users)
  {
    Env = env;
    Logger = logger;
    Conf = conf;

    Session = session;

    Recipe = recipe;
    Content = content;

    Users = users;
  }


  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }
  private IConfiguration Conf { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }

  private IUserService Users { get; }
}
