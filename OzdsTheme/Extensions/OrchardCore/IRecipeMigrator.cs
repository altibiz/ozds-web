using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;

namespace OrchardCore.Themes.OzdsTheme.Extensions.OrchardCore;

public static class IRecipeMigratorExtensions
{
  public static void Execute(
      this IRecipeMigrator schema, string recipe, IDataMigration migration)
  {
    var task = schema.ExecuteAsync(recipe, migration);
    task.Wait();
  }
}
