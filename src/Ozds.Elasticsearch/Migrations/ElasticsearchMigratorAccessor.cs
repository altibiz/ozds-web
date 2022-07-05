
namespace Ozds.Elasticsearch;

public class ElasticsearchMigratorAccessor : IElasticsearchMigratorAccessor
{
  public ElasticsearchMigratorAccessor(
      IElasticsearchMigrator? migrator = null)
  {
    Migrator = migrator;
  }

  public IElasticsearchMigrator? Migrator { get; init; }
}
