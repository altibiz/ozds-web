using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Themes.Ozds.Extensions.OrchardCore;

namespace Ozds.Themes.Ozds.M0;

public static partial class RecipeMigrations
{
  public static void ExecuteLocalization(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/Localization.recipe.json", migration);
  }

  public static void ExecuteLayout(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/Layout.recipe.json", migration);
  }

  public static void ExecuteLayers(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/Layers.recipe.json", migration);
  }

  public static void ExecuteAnonymousRole(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/AnonymousRole.recipe.json", migration);
  }

  public static void ExecuteAdminMenu(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/AdminMenu.recipe.json", migration);
  }

  public static void ExecuteLuceneFullTextSearch(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/LuceneFullTextSearch.recipe.json", migration);
  }
}
