using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds.Extensions.OrchardCore;

public static class IRecipeMigratorExtensions
{
  public static IRecipeMigrator Execute(
      this IRecipeMigrator migrator,
      string recipe,
      IDataMigration migration) =>
    migrator
      .ExecuteAsync(recipe, migration)
      .Block()
      .Return(migrator);
}
