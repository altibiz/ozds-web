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

namespace Members {
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
    return 12;
  }

  public int UpdateFrom1() {
    ContentDefinitionManager.AlterPartDefinition(
        "Offer", part => part.RemoveField("LongDescription") // remove to add
                             .RemoveField("NestoTamo")       // remove to add
    );
    ContentDefinitionManager.MigrateOffer(); return 2;
  }

  public int UpdateFrom2() {
    SchemaBuilder.AddPublished();
    return 3;
  }

  public async Task<int> UpdateFrom3() {
    await RecipeMigrator.ExecuteAsync("pledge.recipe.json", this);
    ContentDefinitionManager.CreatePledge();
    return 4;
  }

  public int UpdateFrom4() {
    ContentDefinitionManager.DefineImageBanner();
    return 5;
  }

  public int UpdateFrom5() {
    ContentDefinitionManager.MigratePayment();
    return 6;
  }

  public int UpdateFrom6() {
    SchemaBuilder.AddPayoutField();
    return 7;
  }

  public int UpdateFrom7() {
    ContentDefinitionManager.MigratePayment();
    return 8;
  }

  public int UpdateFrom8() {
    SchemaBuilder.AddPaymentPublished();
    return 9;
  }

  public int UpdateFrom9() {
    ContentDefinitionManager.AdminPage();
    return 10;
  }

  public int UpdateFrom10() {
    SchemaBuilder.AddTransactionRef();
    return 11;
  }

  public int UpdateFrom11() {
    SchemaBuilder.CreatePaymentByDayIndex();
    return 12;
  }

  public int UpdateFrom12() {
    SchemaBuilder.CreateDeviceIndex();
    return 13;
  }

  public async Task<int> Updatefrom13() {
    await RecipeMigrator.ExecuteAsync("test-owner.recipe.json", this);
    return 14;
  }

  private IRecipeMigrator RecipeMigrator { get; }
  private IContentDefinitionManager ContentDefinitionManager { get; }
  private ISession Session { get; }
}
}