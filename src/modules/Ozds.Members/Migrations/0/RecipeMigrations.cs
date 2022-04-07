using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Members.Extensions.OrchardCore;

namespace Ozds.Members.M0;

public static partial class RecipeMigrations
{
  public static void ExecuteInit(
      this IRecipeMigrator recipe, IDataMigration migration)
  {
    recipe.Execute("0/AuthSettings.recipe.json", migration);
    recipe.Execute("0/CompanyTemplate.recipe.json", migration);
    recipe.Execute("0/CountyTaxonomy.recipe.json", migration);
    recipe.Execute("0/FunctionTaxonomy.recipe.json", migration);
    recipe.Execute("0/MemberContributionTaxonomy.recipe.json", migration);
    recipe.Execute("0/OfferCategoryTaxonomy.recipe.json", migration);
    recipe.Execute("0/OrganizationTypeTaxonomy.recipe.json", migration);
    recipe.Execute("0/SexTaxonomy.recipe.json", migration);
    recipe.Execute("0/UserLandingPageMenu.recipe.json", migration);
    recipe.Execute("0/WorkTaxonomy.recipe.json", migration);
  }
}
