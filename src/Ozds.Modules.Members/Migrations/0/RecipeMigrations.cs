using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.Extensions.OrchardCore;

namespace Ozds.Modules.Members.M0;

public static partial class RecipeMigrations
{
  public static void ExecuteAuthSettings(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/AuthSettings.recipe.json", migration);
  }

  public static void ExecuteUserLandingPageMenu(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/UserLandingPageMenu.recipe.json", migration);
  }

  public static void ExecuteCountyTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/CountyTaxonomy.recipe.json", migration);
  }

  public static void ExecuteCurrencyTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/CurrencyTaxonomy.recipe.json", migration);
  }

  public static void ExecuteArticleTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/ArticleTaxonomy.recipe.json", migration);
  }

  public static void ExecuteCalculationItemStatusTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/CalculationItemStatusTaxonomy.recipe.json", migration);
  }

  public static void ExecuteMeasurementUnitTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/MeasurementUnitTaxonomy.recipe.json", migration);
  }

  public static void ExecutePhaseTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/PhaseTaxonomy.recipe.json", migration);
  }

  public static void ExecuteSiteTypeTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/SiteTypeTaxonomy.recipe.json", migration);
  }

  public static void ExecuteTariffTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/TariffTaxonomy.recipe.json", migration);
  }
}
