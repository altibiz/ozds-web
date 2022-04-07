using System.Threading.Tasks;
using YesSql;
using Microsoft.Extensions.Logging;
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
      IContentDefinitionManager contentDefinitionManager, ISession session,
      ILogger<Migrations> logger) {
    RecipeMigrator = recipeMigrator;
    ContentDefinitionManager = contentDefinitionManager;
    Session = session;
    Logger = logger;
  }

  public async Task<int> CreateAsync() {
    Logger.LogDebug(" >>> Start Member module creation");

    Logger.LogDebug(" >> Start member init.recipe.json");
    await RecipeMigrator.ExecuteAsync("init.recipe.json", this);
    Logger.LogDebug(" >> Start member init.recipe.json");

    Logger.LogDebug(
        " >>  Start Member " + "init.2.content.1.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.1.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.1.Taxonomy.recipe.json");

    Logger.LogDebug(
        " >> Start Member " + "init.2.content.2.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.2.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.2.Taxonomy.recipe.json");

    Logger.LogDebug(
        " >> Start Member " + "init.2.content.3.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.3.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.3.Taxonomy.recipe.json");

    Logger.LogDebug(
        " >> Start Member " + "init.2.content.4.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.4.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.4.Taxonomy.recipe.json");

    Logger.LogDebug(
        " >> Start Member " + "init.2.content.5.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.5.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.5.Taxonomy.recipe.json");

    Logger.LogDebug(
        " >> Start Member " + "init.2.content.6.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.6.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.6.Taxonomy.recipe.json");

    Logger.LogDebug(
        " >> Start Member " + "init.2.content.7.Taxonomy.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.7.Taxonomy.recipe.json", this);
    Logger.LogDebug(
        " >> End Member " + "init.2.content.7.Taxonomy.recipe.json");

    Logger.LogDebug(" >> Start Member " + "init.2.content.8.Menu.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "init.2.content.8.Menu.recipe.json", this);
    Logger.LogDebug(" >> End Member " + "init.2.content.8.Menu.recipe.json");

    Logger.LogDebug(" >> Start Member " + "init.3.Settings.recipe.json");
    await RecipeMigrator.ExecuteAsync("init.3.Settings.recipe.json", this);
    Logger.LogDebug(" >> End Member " + "init.3.Settings.recipe.json");

    Logger.LogDebug(" >> Start Member " + "init.4.Templates.recipe.json");
    await RecipeMigrator.ExecuteAsync("init.4.Templates.recipe.json", this);
    Logger.LogDebug(" >> End Member " + "init.4.Templates.recipe.json");

    Logger.LogDebug(" >> Start Member " + "AlterPersonPart");
    ContentDefinitionManager.AlterPersonPart();
    Logger.LogDebug(" >> End Member " + "AlterPersonPart");

    Logger.LogDebug(" >> Start Member " + "MigratePersonPartIndex");
    SchemaBuilder.MigratePersonPartIndex();
    Logger.LogDebug(" >> End Member " + "MigratePersonPartIndex");

    Logger.LogDebug(" >> Start Member " + "ExecuteMemberMigrations");
    ContentDefinitionManager.ExecuteMemberMigrations();
    Logger.LogDebug(" >> End Member " + "ExecuteMemberMigrations");

    Logger.LogDebug(" >> Start Member " + "MigratePayment");
    ContentDefinitionManager.MigratePayment();
    Logger.LogDebug(" >> End Member " + "MigratePayment");

    Logger.LogDebug(" >> Start Member " + "CreatePaymentIndex");
    SchemaBuilder.CreatePaymentIndex();
    Logger.LogDebug(" >> End Member " + "CreatePaymentIndex");

    Logger.LogDebug(" >> Start Member " + "MigrateOffer");
    ContentDefinitionManager.MigrateOffer();
    Logger.LogDebug(" >> End Member " + "MigrateOffer");

    Logger.LogDebug(" >> Start Member " + "CreateOfferIndex");
    SchemaBuilder.CreateOfferIndex();
    Logger.LogDebug(" >> End Member " + "CreateOfferIndex");

    Logger.LogDebug(" >> Start Member " + "CreateBankStatement");
    ContentDefinitionManager.CreateBankStatement();
    Logger.LogDebug(" >> End Member " + "CreateBankStatement");

    Logger.LogDebug(" >> Start Member " + "pledge.recipe.json");
    await RecipeMigrator.ExecuteAsync("pledge.recipe.json", this);
    Logger.LogDebug(" >> End Member " + "pledge.recipe.json");

    Logger.LogDebug(" >> Start Member " + "CreatePledge");
    ContentDefinitionManager.CreatePledge();
    Logger.LogDebug(" >> End Member " + "CreatePledge");

    Logger.LogDebug(" >> Start Member " + "DefineImageBanner");
    ContentDefinitionManager.DefineImageBanner();
    Logger.LogDebug(" >> End Member " + "DefineImageBanner");

    Logger.LogDebug(" >> Start Member " + "AddPayoutField");
    SchemaBuilder.AddPayoutField();
    Logger.LogDebug(" >> End Member " + "AddPayoutField");

    Logger.LogDebug(" >> Start Member " + "AddPaymentPublished");
    SchemaBuilder.AddPaymentPublished();
    Logger.LogDebug(" >> End Member " + "AddPaymentPublished");

    Logger.LogDebug(" >> Start Member " + "AdminPage");
    ContentDefinitionManager.AdminPage();
    Logger.LogDebug(" >> End Member " + "AdminPage");

    Logger.LogDebug(" >> Start Member " + "AddTransactionRef");
    SchemaBuilder.AddTransactionRef();
    Logger.LogDebug(" >> End Member " + "AddTransactionRef");

    Logger.LogDebug(" >> Start Member " + "CreatePaymentByDayIndex");
    SchemaBuilder.CreatePaymentByDayIndex();
    Logger.LogDebug(" >> End Member " + "CreatePaymentByDayIndex");

    Logger.LogDebug(" >> Start Member " + "CreateDeviceIndex");
    SchemaBuilder.CreateDeviceIndex();
    Logger.LogDebug(" >> End Member " + "CreateDeviceIndex");

    Logger.LogDebug(" >> Start Member " + "test-owner.recipe.json");
    await RecipeMigrator.ExecuteAsync("test-owner.recipe.json", this);
    Logger.LogDebug(" >> End Member " + "test-owner.recipe.json");

    Logger.LogDebug(" >>> End Member module creation");
    return 1;
  }

  private ILogger Logger { get; }

  private IRecipeMigrator RecipeMigrator { get; }
  private IContentDefinitionManager ContentDefinitionManager { get; }
  private ISession Session { get; }
}