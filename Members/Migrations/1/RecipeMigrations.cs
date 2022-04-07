using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Members.Extensions.OrchardCore;

namespace Members.M1;

public static partial class RecipeMigrations
{
  public static void ExecutePledge(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/pledge.recipe.json", migration);
  }
}
