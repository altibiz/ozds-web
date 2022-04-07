using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Members.Extensions.OrchardCore;

namespace Ozds.Members.M1;

public static partial class RecipeMigrations {
  public static void ExecutePledge(
      this IRecipeMigrator recipe, IDataMigration migration) {
    recipe.Execute("1/PledgeTaxonomy.recipe.json", migration);
  }
}
