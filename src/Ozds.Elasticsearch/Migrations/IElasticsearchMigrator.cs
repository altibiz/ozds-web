using Nest;

namespace Ozds.Elasticsearch;

public interface IElasticsearchMigrator
{
  public Task<IIndices>
  MigrateAsync(
      IElasticClient client,
      bool clean = false);

  public IIndices
  Migrate(
      IElasticClient client,
      bool clean = false);
}
