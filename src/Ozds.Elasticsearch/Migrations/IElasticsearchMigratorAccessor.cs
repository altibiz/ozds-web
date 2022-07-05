namespace Ozds.Elasticsearch;

public interface IElasticsearchMigratorAccessor
{
  public IElasticsearchMigrator? Migrator { get; }
}
