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

  public static void ExecuteCountyTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/CountyTaxonomy.recipe.json", migration);
  }

  public static void ExecuteUserLandingPageMenu(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/UserLandingPageMenu.recipe.json", migration);
  }
}
