using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Users.Extensions.OrchardCore;

namespace Ozds.Users.M3;

public static partial class RecipeMigrations
{
  public static void ExecuteOzdsContentDefinitions(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("3/PersonPart.recipe.json", migration);
    recipe.Execute("3/Service.recipe.json", migration);
    recipe.Execute("3/Calculation.recipe.json", migration);
    recipe.Execute("3/Item.recipe.json", migration);
    recipe.Execute("3/Bill.recipe.json", migration);
    recipe.Execute("3/OMM.recipe.json", migration);
    recipe.Execute("3/Member.recipe.json", migration);
    recipe.Execute("3/ZDS.recipe.json", migration);
  }
}
