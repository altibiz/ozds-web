using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;

namespace Ozds.Modules.Members.Extensions.OrchardCore;

public static class IRecipeMigratorExtensions
{
  public static void Execute(
      this IRecipeMigrator schema,
      string recipe,
      IDataMigration migration) =>
    schema.ExecuteAsync(recipe, migration).Wait();
}
