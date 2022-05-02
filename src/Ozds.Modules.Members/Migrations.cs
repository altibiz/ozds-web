using YesSql;
using YesSql.Sql;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;

using Ozds.Modules.Members.M0;

namespace Ozds.Modules.Members;

public sealed class Migrations : DataMigration
{
  public int Create()
  {
    if (Env.IsDevelopment())
    {
      Recipe.ExecuteTestSettings(this);
      Recipe.ExecuteTestTaxonomies(this);
      Content.AlterContent();
      Recipe.ExecuteTestContent(this);
    }
    else
    {
      Recipe.ExecuteSettings(this);
      Recipe.ExecuteTaxonomies(this);
      Content.AlterContent();
    }

    return 1;
  }

  public Migrations(
      IHostEnvironment env,
      IRecipeMigrator recipe,
      IContentDefinitionManager content,
      ISession session)
  {
    Env = env;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  private IHostEnvironment Env { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
