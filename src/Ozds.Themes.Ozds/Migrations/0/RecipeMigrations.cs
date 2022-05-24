using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Themes.Ozds.Extensions.OrchardCore;

namespace Ozds.Themes.Ozds.M0;

public static partial class RecipeMigrations
{
  public static IRecipeMigrator ExecuteLocalization(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/Localization.recipe.json", migration);

  public static IRecipeMigrator ExecuteLayers(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/Layers.recipe.json", migration);

  public static IRecipeMigrator ExecuteLuceneFullTextSearch(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/LuceneFullTextSearch.recipe.json", migration);

  public static IRecipeMigrator ExecuteSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteLocalization(migration)
      .ExecuteLayers(migration)
      .ExecuteLuceneFullTextSearch(migration);

  public static IRecipeMigrator ExecuteTestSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteLocalization(migration)
      .ExecuteLayers(migration)
      .ExecuteLuceneFullTextSearch(migration);


  public static IRecipeMigrator ExecuteFrontPage(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/FrontPage.recipe.json", migration);

  public static IRecipeMigrator ExecuteMainMenu(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/MainMenu.recipe.json", migration);

  public static IRecipeMigrator ExecuteContent(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteFrontPage(migration)
      .ExecuteMainMenu(migration);

  public static IRecipeMigrator ExecuteTestContent(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe
      .ExecuteFrontPage(migration)
      .ExecuteMainMenu(migration);
}
