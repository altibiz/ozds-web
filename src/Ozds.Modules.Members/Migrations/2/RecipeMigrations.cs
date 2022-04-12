using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Users.Extensions.OrchardCore;

namespace Ozds.Users.M2;

public static partial class RecipeMigrations
{
  public static void ExecuteTestOwner(this IRecipeMigrator recipe,
      IDataMigration migration, bool isDevelopment)
  {
    if (isDevelopment)
    {
      recipe.Execute("2/TestOwner.recipe.json", migration);
    }
  }
}
