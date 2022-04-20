using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;

namespace Ozds.Modules.Members.Extensions.OrchardCore;

public static class IRecipeMigratorExtensions
{
  public static IRecipeMigrator Execute(
      this IRecipeMigrator migrator,
      string recipe,
      IDataMigration migration)
  {
    migrator
      .ExecuteAsync(recipe, migration)
      .Wait();

    return migrator;
  }
}
