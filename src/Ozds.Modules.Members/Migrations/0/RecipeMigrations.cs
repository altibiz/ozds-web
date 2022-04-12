using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Members.Extensions.OrchardCore;

namespace Ozds.Modules.Members.M0;

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
    recipe.Execute("0/Bill.recipe.json", migration);
    recipe.Execute("0/Calculation.recipe.json", migration);
    recipe.Execute("0/Item.recipe.json", migration);
    recipe.Execute("0/Member.recipe.json", migration);
    recipe.Execute("0/OMM.recipe.json", migration);
    recipe.Execute("0/PersonPart.recipe.json", migration);
    recipe.Execute("0/PledgeTaxonomy.recipe.json", migration);
    recipe.Execute("0/Service.recipe.json", migration);
    recipe.Execute("0/TestOwner.recipe.json", migration);
    recipe.Execute("0/ZDS.recipe.json", migration);
  }
}
