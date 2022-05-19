using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Themes.Ozds.M0;

namespace Ozds.Themes.Ozds;

public partial class Migrations : DataMigration
{
  public int Create()
  {
    if (Env.IsDevelopment())
    {
      Recipe.ExecuteTestSettings(this);
      Content.AlterContent();
      Recipe.ExecuteTestContent(this);
    }
    else
    {
      Recipe.ExecuteSettings(this);
      Content.AlterContent();
      Recipe.ExecuteContent(this);
    }

    return 1;
  }

  public Migrations(
      IHostEnvironment env,
      ILogger<Migrations> logger,
      IRecipeMigrator recipe,
      IContentDefinitionManager content,
      ISession session)
  {
    Env = env;
    Logger = logger;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private ISession Session { get; }
  private ISchemaBuilder Schema { get => SchemaBuilder; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
