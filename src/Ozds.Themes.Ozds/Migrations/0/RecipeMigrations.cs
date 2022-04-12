using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using OrchardCore.Themes.Ozds.Extensions.OrchardCore;

namespace OrchardCore.Themes.Ozds.M0;

public static partial class RecipeMigrations
{
  public static void ExecuteInit(this IRecipeMigrator recipe,
      IDataMigration migration, bool isDevelopment)
  {
    recipe.Execute("0/AboutUsArticle.recipe.json", migration);
    recipe.Execute("0/AdminMenu.recipe.json", migration);
    recipe.Execute("0/AnonymousRole.recipe.json", migration);
    recipe.Execute("0/BannerImageProfile.recipe.json", migration);
    recipe.Execute("0/BlogContentDefinitions.recipe.json", migration);
    recipe.Execute("0/BlogLuceneIndices.recipe.json", migration);
    recipe.Execute("0/Footer.recipe.json", migration);
    recipe.Execute("0/HomePageEn.recipe.json", migration);
    recipe.Execute("0/HomePageHr.recipe.json", migration);
    recipe.Execute("0/Layers.recipe.json", migration);
    recipe.Execute("0/Layout.recipe.json", migration);
    recipe.Execute("0/Localization.recipe.json", migration);
    recipe.Execute("0/LuceneFullTextSearch.recipe.json", migration);
    recipe.Execute("0/Media.recipe.json", migration);
    recipe.Execute("0/MenuEn.recipe.json", migration);
    recipe.Execute("0/MenuHr.recipe.json", migration);
    recipe.Execute("0/RecentBlogPostsQuery.recipe.json", migration);
    recipe.Execute("0/SendEmailWorkflow.recipe.json", migration);
    recipe.Execute("0/WhatIsNewBlog.recipe.json", migration);

    if (isDevelopment)
    {
      recipe.Execute("0/TestBlogPost.recipe.json", migration);
    }
  }
}
