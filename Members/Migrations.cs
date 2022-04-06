using System.Threading.Tasks;
using YesSql;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Members.Persons;
using Members.Core;
using Members.Payments;
using Members.Indexes;
using Members.Base;
using Members.Devices;

namespace Members;

public class Migrations : DataMigration {
  public Migrations(IRecipeMigrator recipeMigrator,
      IContentDefinitionManager contentDefinitionManager, ISession session) {
    RecipeMigrator = recipeMigrator;
    ContentDefinitionManager = contentDefinitionManager;
    Session = session;
  }

  public async Task<int> CreateAsync() {
    await RecipeMigrator.ExecuteAsync("init.recipe.json", this);

#region PersonPart
    ContentDefinitionManager.AlterPersonPart();
    SchemaBuilder.MigratePersonPartIndex();
#endregion

    ContentDefinitionManager.ExecuteMemberMigrations();
    ContentDefinitionManager.MigratePayment();
    SchemaBuilder.CreatePaymentIndex();
    ContentDefinitionManager.MigrateOffer();
    SchemaBuilder.CreateOfferIndex();
    ContentDefinitionManager.CreateBankStatement();
    await RecipeMigrator.ExecuteAsync("pledge.recipe.json", this);
    ContentDefinitionManager.CreatePledge();
    ContentDefinitionManager.DefineImageBanner();
    SchemaBuilder.AddPayoutField();
    SchemaBuilder.AddPaymentPublished();
    ContentDefinitionManager.AdminPage();
    SchemaBuilder.AddTransactionRef();
    SchemaBuilder.CreatePaymentByDayIndex();
    SchemaBuilder.CreateDeviceIndex();
    await RecipeMigrator.ExecuteAsync("test-owner.recipe.json", this);
    return 1;
  }

  private IRecipeMigrator RecipeMigrator { get; }
  private IContentDefinitionManager ContentDefinitionManager { get; }
  private ISession Session { get; }
}