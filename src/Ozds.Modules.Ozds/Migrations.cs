using YesSql;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Ozds.Modules.Ozds.Extensions.OrchardCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

using Ozds.Modules.Ozds.M0;

namespace Ozds.Modules.Ozds;

public sealed class Migrations : DataMigration
{
  public int Create()
  {
    if (Env.IsDevelopment())
    {
      Schema.CreateIndexMapTables();
      Schema.CreateIndexMapIndices();
      Recipe.ExecuteDevSettings(this);
      Recipe.ExecuteDevTaxonomies(this);
      Content.AlterContent();

      if (Conf
            .GetSection("Ozds")
            .GetSection("Modules")
            .GetSection("Ozds")
            .GetValue<object?>("IsDemo") is not null)
      {
        Session.SaveDemoData();
        Recipe.ExecuteDemoContent(this);
        Recipe.Execute("Sensitive/demo.recipe.json", this);
      }
      else
      {
        Session.SaveDevData();
        Recipe.ExecuteDevContent(this);
      }
    }
    else
    {
      Schema.CreateIndexMapTables();
      Schema.CreateIndexMapIndices();
      Recipe.ExecuteSettings(this);
      Recipe.ExecuteTaxonomies(this);
      Content.AlterContent();
      Session.SaveData();
      Recipe.ExecuteContent(this);
    }

    return 1;
  }

  public Migrations(
      IHostEnvironment env,
      IConfiguration conf,
      ILogger<Migrations> log,
      IRecipeMigrator recipe,
      IContentDefinitionManager content,
      ISession session)
  {
    Env = env;
    Conf = conf;
    Log = log;

    Session = session;

    Recipe = recipe;
    Content = content;
  }

  private IConfiguration Conf { get; }
  private IHostEnvironment Env { get; }
  private ILogger Log { get; }

  private ISchemaBuilder Schema { get => SchemaBuilder; }

  [System.Diagnostics.CodeAnalysis.SuppressMessage(
    "CodeQuality",
    "IDE0052:Remove unread private members",
    Justification = "Might need it at some point.")]
  private ISession Session { get; }

  private IRecipeMigrator Recipe { get; }
  private IContentDefinitionManager Content { get; }
}
