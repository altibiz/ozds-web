using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.Extensions.OrchardCore;

namespace Ozds.Modules.Members.M2;

public static partial class RecipeMigrations {
  public static void ExecuteTestOwner(this IRecipeMigrator recipe,
      IDataMigration migration, bool isDevelopment) {
    if (isDevelopment) {
      recipe.Execute("2/TestOwner.recipe.json", migration);
    }
  }
}
