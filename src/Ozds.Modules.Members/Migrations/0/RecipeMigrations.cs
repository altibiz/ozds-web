using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.Extensions.OrchardCore;

namespace Ozds.Modules.Members.M0;

public static partial class RecipeMigrations
{
  public static IRecipeMigrator ExecuteAuthSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/AuthSettings.recipe.json", migration);

  public static IRecipeMigrator ExecuteSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteAuthSettings(migration);

  public static IRecipeMigrator ExecuteTestSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteAuthSettings(migration);

  public static IRecipeMigrator ExecuteTariffModelTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TariffModelTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteTariffElementTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TariffElementTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteTariffItemTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TariffItemTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteSiteMeasurementSourceTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/SiteMeasurementSourceTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestSiteMeasurementSourceTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute(
        "0/TestSiteMeasurementSourceTaxonomy.recipe.json",
        migration);

  public static IRecipeMigrator ExecuteSiteStatusTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/SiteStatusTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteTaxonomies(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteTariffModelTaxonomy(migration)
      .ExecuteTariffElementTaxonomy(migration)
      .ExecuteTariffItemTaxonomy(migration)
      .ExecuteSiteMeasurementSourceTaxonomy(migration)
      .ExecuteSiteStatusTaxonomy(migration);

  public static IRecipeMigrator ExecuteTestTaxonomies(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteTariffModelTaxonomy(migration)
      .ExecuteTariffElementTaxonomy(migration)
      .ExecuteTariffItemTaxonomy(migration)
      .ExecuteTestSiteMeasurementSourceTaxonomy(migration)
      .ExecuteSiteStatusTaxonomy(migration);

  public static IRecipeMigrator ExecuteOperatorCatalogue(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/OperatorCatalogue.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestCenter1(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestCenter1.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestConsumerSite1(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestConsumerSite1.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestConsumer1(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestConsumer1.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestReceipt1(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestReceipt1.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestReceiptRevision1(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestReceiptRevision1.recipe.json", migration);
  public static IRecipeMigrator ExecuteContent(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteOperatorCatalogue(migration);

  public static IRecipeMigrator ExecuteTestContent(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteOperatorCatalogue(migration)
      .ExecuteTestCenter1(migration)
      .ExecuteTestConsumerSite1(migration)
      .ExecuteTestConsumer1(migration)
      .ExecuteTestReceiptRevision1(migration);
}
