using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using OrchardCore.Themes.OzdsTheme.M0;
using OrchardCore.Themes.OzdsTheme.M1;

namespace OrchardCore.Themes.OzdsTheme;

public partial class Migrations : DataMigration {
  public Migrations(IHostEnvironment env, ILogger<Migrations> logger,
      IRecipeMigrator recipe, IContentDefinitionManager content,
      ISession session) {
    Env = env;
    Logger = logger;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  public int Create() {
    Recipe.ExecuteInit(this, Env.IsDevelopment());

    return 1;
  }

  public int UpdateFrom1() {
    Content.AlterGPiecePart();
    Content.AlterGPieceType();
    Content.AlterGalleryPart();
    Content.AlterGalleryType();
    Content.AlterBlogPostType();
    Content.AlterBlogPostPart();

    return 2;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private ISession Session { get; }
  private ISchemaBuilder Schema { get => SchemaBuilder; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
