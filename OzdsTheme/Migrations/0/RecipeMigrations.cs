using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using OrchardCore.Themes.OzdsTheme.Extensions.OrchardCore;

namespace OrchardCore.Themes.OzdsTheme.M0;

public static partial class RecipeMigrations {
  public static void ExecuteInit(
      this IRecipeMigrator recipe, IDataMigration migration) {
    recipe.Execute("0/ozds.1.settings.recipe.json", migration);
    recipe.Execute("0/ozds.2.ContentDefinition.recipe.json", migration);
    recipe.Execute("0/ozds.3.lucene-index.recipe.json", migration);
    recipe.Execute("0/ozds.4.Settings.recipe.json", migration);
    recipe.Execute("0/ozds.5.Roles.recipe.json", migration);
    recipe.Execute("0/ozds.6.media.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.1.Menu.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.2.Menu.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.3.Blog.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.4.BlogPost.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.5.Article.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.6.RawHtml.recipe.json", migration);
    recipe.Execute("0/ozds.7.content.7.Page.recipe.json", migration);
    recipe.Execute("0/ozds.8.layers.recipe.json", migration);
    recipe.Execute("0/ozds.9.queries.recipe.json", migration);
    recipe.Execute("0/ozds.10.AdminMenu.recipe.json", migration);
    recipe.Execute("0/ozds.11.MediaProfiles.recipe.json", migration);
    recipe.Execute("0/ozds.12.WorkflowType.recipe.json", migration);
    recipe.Execute("0/tags-cats.recipe.json", migration);
    recipe.Execute("0/localization.recipe.json", migration);
    recipe.Execute("0/localizemenu.recipe.json", migration);
  }
}
