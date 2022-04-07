using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Members.Extensions.OrchardCore;

namespace Members.M0;

public static partial class RecipeMigrations {
  public static void ExecuteInit(
      this IRecipeMigrator recipe, IDataMigration migration) {
    recipe.Execute("0/init.recipe.json", migration);
    recipe.Execute("0/init.1.content.1.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.2.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.3.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.4.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.5.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.6.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.7.Taxonomy.recipe.json", migration);
    recipe.Execute("0/init.1.content.8.Menu.recipe.json", migration);
    recipe.Execute("0/init.2.Settings.recipe.json", migration);
    recipe.Execute("0/init.3.Templates.recipe.json", migration);
  }
}
