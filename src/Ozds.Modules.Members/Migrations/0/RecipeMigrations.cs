using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.Extensions.OrchardCore;

namespace Ozds.Modules.Members.M0;

public static partial class RecipeMigrations
{
  public static IRecipeMigrator ExecuteAuthSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/AuthSettings.recipe.json", migration);

  public static IRecipeMigrator ExecuteUserLandingPageMenu(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/UserLandingPageMenu.recipe.json", migration);

  public static IRecipeMigrator ExecuteCountyTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/CountyTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteCurrencyTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/CurrencyTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteArticleTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/ArticleTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteCalculationItemStatusTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/CalculationItemStatusTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteMeasurementUnitTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/MeasurementUnitTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecutePhaseTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/PhaseTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteSiteTypeTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/SiteTypeTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteTariffTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TariffTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteCommandCreateTestUsers(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/CommandCreateTestUsers.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestCenter(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestCenter.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestMember(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestMember.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestReceipt(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestReceipt.recipe.json", migration);
}
