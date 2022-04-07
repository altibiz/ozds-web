using Microsoft.Extensions.Logging;
using OrchardCore.Autoroute.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Data.Migration;
using OrchardCore.Flows.Models;
using OrchardCore.Indexing;
using OrchardCore.Media.Settings;
using OrchardCore.Recipes.Services;
using OrchardCore.Taxonomies.Settings;
using System.Threading.Tasks;
using YesSql;

namespace OrchardCore.Themes.OzdsTheme;

public class Migrations : DataMigration
{
  public Migrations(IRecipeMigrator recipeMigrator,
      IContentDefinitionManager contentDefinitionManager, ISession session,
      ILogger<Migrations> logger)
  {
    RecipeMigrator = recipeMigrator;
    ContentDefinitionManager = contentDefinitionManager;
    Session = session;
    Logger = logger;
  }

  public async Task<int> CreateAsync()
  {
    Logger.LogDebug(" >>> Start OzdsTheme creation");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.1.settings.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.1.settings.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.1.settings.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.2.ContentDefinition.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.2.ContentDefinition.recipe.json", this);
    Logger.LogDebug(
        " >> End OzdsTheme " + "ozds.2.ContentDefinition.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.3.lucene-index.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.3.lucene-index.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.3.lucene-index.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.4.Settings.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.4.Settings.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.4.Settings.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.5.Roles.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.5.Roles.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.5.Roles.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.6.media.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.6.media.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.6.media.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.1.Menu.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.1.Menu.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.7.content.1.Menu.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.2.Menu.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.2.Menu.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.7.content.2.Menu.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.5.Blog.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.5.Blog.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.7.content.5.Blog.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.6.BlogPost.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.6.BlogPost.recipe.json", this);
    Logger.LogDebug(
        " >> End OzdsTheme " + "ozds.7.content.6.BlogPost.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.7.Article.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.7.Article.recipe.json", this);
    Logger.LogDebug(
        " >> End OzdsTheme " + "ozds.7.content.7.Article.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.8.RawHtml.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.8.RawHtml.recipe.json", this);
    Logger.LogDebug(
        " >> End OzdsTheme " + "ozds.7.content.8.RawHtml.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.7.content.9.Page.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.7.content.9.Page.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.7.content.9.Page.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.8.layers.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.8.layers.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.8.layers.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.9.queries.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.9.queries.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.9.queries.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "ozds.10.AdminMenu.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.10.AdminMenu.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.10.AdminMenu.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.11.MediaProfiles.recipe.json");
    await RecipeMigrator.ExecuteAsync(
        "ozds.11.MediaProfiles.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.11.MediaProfiles.recipe.json");

    Logger.LogDebug(
        " >> Start OzdsTheme " + "ozds.12.WorkflowType.recipe.json");
    await RecipeMigrator.ExecuteAsync("ozds.12.WorkflowType.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "ozds.12.WorkflowType.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "GPiece");
    ContentDefinitionManager.AlterPartDefinition("GPiece",
        cfg =>
            cfg.WithDescription("Contains the fields for the current type")
                .WithField(
                    "Caption", fieldBuilder => fieldBuilder.OfType("HtmlField")
                                                   .WithDisplayName("Caption")
                                                   .WithEditor("Wysiwyg"))
                .WithField("DisplayCaption",
                    fieldBuilder => fieldBuilder.OfType("BooleanField")
                                        .WithDisplayName("Display Caption"))
                .WithField("Image",
                    fieldBuilder => fieldBuilder.OfType("MediaField")
                                        .WithDisplayName("Image")
                                        .WithSettings(new MediaFieldSettings
                                        {
                                          Required = true,
                                          Multiple = false
                                        }))
                .WithField("ImageClass",
                    fieldBuilder => fieldBuilder.OfType("TextField")
                                        .WithDisplayName("Image Class"))
                .WithField("ImageAltText",
                    fieldBuilder => fieldBuilder.OfType("TextField")
                                        .WithDisplayName("Image Alt Text")));

    ContentDefinitionManager.AlterTypeDefinition(
        "GPiece", type => type.WithPart("GPiece"));

    ContentDefinitionManager.AlterPartDefinition(
        "GPiece", cfg => cfg.WithField(
                      "Link", fieldBuilder => fieldBuilder.OfType("TextField")
                                                  .WithDisplayName("Link")
                                                  .WithEditor("Url")));
    Logger.LogDebug(" >> End OzdsTheme " + "GPiece");

    Logger.LogDebug(" >> start OzdsTheme " + "Gallery");
    ContentDefinitionManager.AlterPartDefinition("Gallery",
        cfg =>
            cfg.WithDescription("Contains the fields for the current type")
                .WithField("DisplayType",
                    fieldBuilder => fieldBuilder.OfType("TextField")
                                        .WithDisplayName("Display Type")));

    ContentDefinitionManager.AlterTypeDefinition("Gallery",
        type =>
            type.WithPart("TitlePart")
                .WithPart("Gallery")
                .WithPart("GPieces", "BagPart",
                    cfg => cfg.WithDisplayName("GPieces")
                               .WithDescription("GPieces to display in the.")
                               .WithSettings(
                                   new BagPartSettings
                                   {
                                     ContainedContentTypes =
                                                             new[] { "GPiece" },
                                     DisplayType = "Detail"
                                   }))
                .Stereotype("Widget"));
    Logger.LogDebug(" >> End OzdsTheme " + "Gallery");

    Logger.LogDebug(" >> Start OzdsTheme " + "Taxonomy");
    var ci = await Session
                 .Query<ContentItem, ContentItemIndex>(
                     x => x.ContentType == "Taxonomy" &&
                          x.DisplayText == "Categories")
                 .FirstOrDefaultAsync();
    if (ci != null) Session.Delete(ci);
    ci =
        await Session
            .Query<ContentItem, ContentItemIndex>(
                x => x.ContentType == "Taxonomy" && x.DisplayText == "Tags")
            .FirstOrDefaultAsync();
    if (ci != null) Session.Delete(ci); await Session.SaveChangesAsync();
    Logger.LogDebug(" >> End OzdsTheme " + "Taxonomy");

    Logger.LogDebug(" >> Start OzdsTheme " + "BlogPost");
    ContentDefinitionManager.AlterTypeDefinition("BlogPost",
        type =>
            type.RemovePart("MarkdownBodyPart")
                .DisplayedAs("Blog Post")
                .Draftable()
                .Versionable()
                .WithPart("TitlePart", part => part.WithPosition("0"))
                .WithPart("AutoroutePart",
                    part => part.WithPosition("2").WithSettings(
                        new AutoroutePartSettings
                        {
                          AllowCustomPath = true,
                          Pattern =
                              "{{ Model.ContentItem | container | display_text | slugify }}/{{ Model.ContentItem | display_text | slugify }}",
                          ShowHomepageOption = false,
                        }))
                .WithPart("BlogPost", part => part.WithPosition("3"))
                .WithPart("HtmlBodyPart",
                    part => part.WithPosition("1").WithEditor("Wysiwyg")));

    ContentDefinitionManager.AlterPartDefinition(
        "BlogPost", part => part.RemoveField("Category").RemoveField("Tags"));

    ContentDefinitionManager.AlterPartDefinition("BlogPost",
        part =>
            part.WithField("Subtitle", field => field.OfType("TextField")
                                                    .WithDisplayName("Subtitle")
                                                    .WithPosition("0"))
                .WithField("Image",
                    field =>
                        field.OfType("MediaField")
                            .WithDisplayName("Banner Image")
                            .WithPosition("1")
                            .WithSettings(
                                new ContentIndexSettings
                                {
                                  Included = false,
                                  Stored = false,
                                  Analyzed = false
                                })
                            .WithSettings(new MediaFieldSettings
                            {
                              Multiple = false,
                              AllowAnchors = true,
                            }))
                .WithField("Tags",
                    field => field.OfType("TaxonomyField")
                                 .WithDisplayName("Tags")
                                 .WithEditor("Tags")
                                 .WithDisplayMode("Tags")
                                 .WithPosition("2")
                                 .WithSettings(new TaxonomyFieldSettings
                                 {
                                   TaxonomyContentItemId =
                                       "45j76cwwz4f4v4hx5zqxfpzvwq",
                                 }))
                .WithField("Category",
                    field => field.OfType("TaxonomyField")
                                 .WithDisplayName("Category")
                                 .WithPosition("3")
                                 .WithSettings(new TaxonomyFieldSettings
                                 {
                                   TaxonomyContentItemId =
                                       "4dgj6ce33vdsbxqz8hw4c4c24d",
                                   Unique = true,
                                   LeavesOnly = true,
                                 })));
    Logger.LogDebug(" >> End OzdsTheme " + "BlogPost");

    Logger.LogDebug(" >> Start OzdsTheme " + "tags-cats.recipe.json");
    await RecipeMigrator.ExecuteAsync("tags-cats.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "tags-cats.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "localization.recipe.json");
    await RecipeMigrator.ExecuteAsync("localization.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "localization.recipe.json");

    Logger.LogDebug(" >> Start OzdsTheme " + "localizemenu.recipe.json");
    await RecipeMigrator.ExecuteAsync("localizemenu.recipe.json", this);
    Logger.LogDebug(" >> End OzdsTheme " + "localizemenu.recipe.json");

    Logger.LogDebug(" >>> End OzdsTheme creation"); return 1;
  }

  private ILogger Logger { get; }

  private IRecipeMigrator RecipeMigrator { get; }
  private IContentDefinitionManager ContentDefinitionManager { get; }
  private ISession Session { get; }
}