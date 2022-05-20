using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Util;

namespace Ozds.Modules.Ozds.Extensions.OrchardCore;

public static class IRecipeMigratorExtensions
{
  public static IRecipeMigrator Execute(
      this IRecipeMigrator migrator,
      string recipe,
      IDataMigration migration) =>
    migrator
      .ExecuteAsync(recipe, migration)
      .BlockTask()
      .Return(migrator);
}
