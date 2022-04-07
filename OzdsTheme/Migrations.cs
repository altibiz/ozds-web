using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using OrchardCore.Themes.OzdsTheme.M0;
using OrchardCore.Themes.OzdsTheme.M1;

namespace OrchardCore.Themes.OzdsTheme;

public partial class Migrations : DataMigration
{
  public Migrations(IRecipeMigrator recipe, IContentDefinitionManager content,
      ISession session, ILogger<Migrations> logger)
  {
    Recipe = recipe;
    Content = content;
    Session = session;
    Logger = logger;
  }

  public int Create()
  {
    Recipe.ExecuteInit(this);

    return 1;
  }

  public int UpdateFrom1()
  {
    Content.AlterGPiecePart();
    Content.AlterGPieceType();
    Content.AlterGalleryPart();
    Content.AlterGalleryType();
    Content.AlterBlogPostType();
    Content.AlterBlogPostPart();

    return 2;
  }

  private ILogger Logger { get; }
  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
  private ISession Session { get; }
  private ISchemaBuilder Schema { get => SchemaBuilder; }
}
