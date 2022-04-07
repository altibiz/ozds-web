using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Members.Extensions.OrchardCore;

namespace Members.M2;

public static partial class RecipeMigrations {
  public static void ExecuteTestOwner(
      this IRecipeMigrator recipe, IDataMigration migration) {
    recipe.Execute("0/test-owner.recipe.json", migration);
  }
}
