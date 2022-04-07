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

    await RecipeMigrator.ExecuteAsync("0/ozds.1.settings.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.2.ContentDefinition.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.3.lucene-index.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/ozds.4.Settings.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/ozds.5.Roles.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/ozds.6.media.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.1.Menu.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.2.Menu.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.3.Blog.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.4.BlogPost.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.5.Article.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.6.RawHtml.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.7.content.7.Page.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/ozds.8.layers.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/ozds.9.queries.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/ozds.10.AdminMenu.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.11.MediaProfiles.recipe.json", this);

    await RecipeMigrator.ExecuteAsync(
        "0/ozds.12.WorkflowType.recipe.json", this);

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

    ContentDefinitionManager.AlterPartDefinition("Gallery",
        cfg => cfg.WithDescription("Contains the fields for the current type")
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

    await RecipeMigrator.ExecuteAsync("0/tags-cats.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/localization.recipe.json", this);

    await RecipeMigrator.ExecuteAsync("0/localizemenu.recipe.json", this);

    return 1;
  }

  private ILogger Logger { get; }

  private IRecipeMigrator RecipeMigrator { get; }
  private IContentDefinitionManager ContentDefinitionManager { get; }
  private ISession Session { get; }
}