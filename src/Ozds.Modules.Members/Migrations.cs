using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.M0;

namespace Ozds.Modules.Members;

public sealed class Migrations : DataMigration {
  public Migrations(IHostEnvironment env, ILogger<Migrations> logger,
      IRecipeMigrator recipe, IContentDefinitionManager content,
      ISession session) {
    Env = env;
    Logger = logger;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  public int Create() {
    Recipe.ExecuteInit(this);

    Content.AlterAdminPageType();

    Content.AlterCompanyPart();
    Content.AlterCompanyType();
    Content.AlterOfferPart();
    Content.AlterOfferType();
    Content.AlterPersonPart();
    Content.AlterMemberPart();
    Content.AlterMemberType();
    Schema.CreateOfferIndex();
    Schema.CreatePersonPartIndex();
    Schema.AlterPersonPartIndex();

    Content.AlterPaymentPart();
    Content.AlterPaymentType();
    Schema.CreatePaymentIndex();
    Schema.AlterPaymentIndex();
    Schema.CreatePaymentByDayIndex();

    Content.AlterBankStatementPart();
    Content.AlterBankStatementType();

    Content.AlterImagePart();
    Content.AlterImageType();

    Content.AlterPledgePart();
    Content.AlterPledgeType();
    Content.AlterPledgeVariantPart();
    Content.AlterPledgeVariantType();

    Schema.CreateDeviceIndex();

    return 1;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
