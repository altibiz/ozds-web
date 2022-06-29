using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public class ElasticsearchMigratorAccessor
{
  public ElasticsearchMigratorAccessor(
      IElasticsearchMigrator? migrator = null)
  {
    Migrator = migrator;
  }

  public IElasticsearchMigrator? Migrator { get; init; }
}

public interface IElasticsearchMigrator
{
  public Task<Indices> MigrateAsync(IElasticClient client);

  public Indices Migrate(IElasticClient client);
}

public class ElasticsearchMigrator : IElasticsearchMigrator
{
  public Task<Indices> MigrateAsync(IElasticClient client) =>
    DiscoverIndicesAsync(client, Env.IsDevelopment())
      .ThenAwait(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Select(migration => oldIndices
          .Yield()
          .Select(oldIndex =>
            MigrateAsync(
              client,
              oldIndex,
              migration.Version,
              migration.Store))
          .AwaitValue()
          .Then(IndexExtensions.Indices))
        .AwaitValue()
        .Then(indices => indices
          .LastOrDefault()));

  public Indices Migrate(IElasticClient client) =>
    DiscoverIndices(client, Env.IsDevelopment())
      .Var(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Select(migration => oldIndices
          .Yield()
          .Select(oldIndex =>
            Migrate(
              client,
              oldIndex,
              migration.Version,
              migration.Store))
          .Var(IndexExtensions.Indices))
        .Var(indices => indices
          .LastOrDefault()));

  private async Task<Index> MigrateAsync(
      IElasticClient client,
      Index oldIndex,
      int newVersion,
      IMigrationStore store,
      bool clean = false)
  {
    if (oldIndex.Version < newVersion &&
        store.Processors.TryGetValue(
          oldIndex.Base,
          out var indexProcessors))
    {
      var newIndex = oldIndex.WithVersion(newVersion);

      await client.Ingest
        .PutPipelineAsync(new Id(newIndex.Name), p => p
          .Processors(indexProcessors));

      await client
        .ReindexOnServerAsync(r => r
          .WaitForCompletion()
          .Source(s => s
            .Index(oldIndex.Name))
          .Destination(d => d
            .Index(newIndex.Name)
            .Pipeline(newIndex.Name)));

      if (clean)
      {
        await client.Ingest
          .DeletePipelineAsync(new Id(newIndex.Name));

        await client.Indices
          .DeleteAsync(oldIndex.Name);
      }

      return newIndex;
    }

    return oldIndex;
  }

  private Index Migrate(
      IElasticClient client,
      Index oldIndex,
      int newVersion,
      IMigrationStore store,
      bool clean = false)
  {
    if (oldIndex.Version < newVersion &&
        store.Processors.TryGetValue(
          oldIndex.Base,
          out var indexProcessors))
    {
      var newIndex = oldIndex.WithVersion(newVersion);

      client.Ingest
        .PutPipeline(new Id(newIndex.Name), p => p
          .Processors(indexProcessors));

      client
        .ReindexOnServer(r => r
          .WaitForCompletion()
          .Source(s => s
            .Index(oldIndex.Name))
          .Destination(d => d
            .Index(newIndex.Name)
            .Pipeline(newIndex.Name)));

      if (clean)
      {
        client.Ingest
          .DeletePipeline(new Id(newIndex.Name));

        client.Indices
          .Delete(oldIndex.Name);
      }

      return newIndex;
    }

    return oldIndex;
  }

  public ElasticsearchMigrator(
      ILogger<ElasticsearchMigrator> log,
      IHostEnvironment env)
  {
    Log = log;
    Env = env;
  }

  private ILogger Log { get; }
  private IHostEnvironment Env { get; }

  private static IEnumerable<(int Version, IMigrationStore Store)>
  DiscoverMigrations(Type migrationType) =>
    migrationType
      .GetMethods(
        System.Reflection.BindingFlags.Public |
        System.Reflection.BindingFlags.Static)
      .SelectFilter(migration => migration.Name
        .RegexReplace(
          @"^apply([0-9]+)$", @"$1",
          System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        .TryParseInt()
        .WhenNonNull(version => migration
          .TryInvoke(
            null,
            new[] { new MigrationStore().As<IMigrationStore>() })
          .As<IMigrationStore>()
          .WhenNonNull(store => (version, store))))
      .OrderBy(versionAndStore => versionAndStore.version);

  private static Task<Indices>
  DiscoverIndicesAsync(
      IElasticClient client,
      bool isDev,
      int? version = null) =>
    client.Cat
      .IndicesAsync(i => i
        .Index(Nest.Indices.Parse($"{Index.MakePrefix(isDev)}*")))
      .Then(indices =>
        new Indices(
          indices.Records.Select(record => record.Index),
          isDev,
          version));

  private static Indices
  DiscoverIndices(
      IElasticClient client,
      bool isDev,
      int? version = null) =>
    client.Cat
      .Indices(i => i
        .Index(Nest.Indices.Parse($"{Index.MakePrefix(isDev)}*")))
      .Var(indices =>
        new Indices(
          indices.Records.Select(record => record.Index),
          isDev,
          version));
}
