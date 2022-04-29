using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.Extensions.OrchardCore;

namespace Ozds.Modules.Members.M0;

public static partial class RecipeMigrations
{
  public static IRecipeMigrator ExecuteAuthSettings(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/AuthSettings.recipe.json", migration);

  public static IRecipeMigrator ExecuteTariffElementTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TariffElementTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteSiteMeasurementSourceTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/SiteMeasurementSourceTaxonomy.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestSiteMeasurementSourceTaxonomy(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute(
        "0/TestSiteMeasurementSourceTaxonomy.recipe.json",
        migration);

  public static IRecipeMigrator ExecuteTestCenter(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestCenter.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestCenterSite(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestCenterSite.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestMemberSite(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestMemberSite.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestMember(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestMember.recipe.json", migration);

  public static IRecipeMigrator ExecuteTestReceipt(
      this IRecipeMigrator recipe, IDataMigration migration) =>
    recipe.Execute("0/TestReceipt.recipe.json", migration);
}
