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
        .Var(migrations => migrations
          .Select((migration, index) => oldIndices
            .Yield()
            .Select(oldIndex =>
              MigrateAsync(
                client,
                oldIndex,
                migration.Version,
                migration.Store,
                (index == migrations.Count() - 1) ?
                  Index.GetCreator(oldIndex)
                : null))
            .AwaitValue()
            .Then(IndexExtensions.Indices))
          .AwaitValue()
          .Then(indices => indices
            .LastOrDefault())));

  public Indices Migrate(IElasticClient client) =>
    DiscoverIndices(client, Env.IsDevelopment())
      .Var(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Var(migrations => migrations
          .Select((migration, index) => oldIndices
            .Yield()
            .Select(oldIndex =>
              Migrate(
                client,
                oldIndex,
                migration.Version,
                migration.Store,
                (index == migrations.Count() - 1) ?
                  Index.GetCreator(oldIndex)
                : null))
            .Var(IndexExtensions.Indices))
          .Var(indices => indices
            .LastOrDefault())));

  private async Task<Index> MigrateAsync(
      IElasticClient client,
      Index oldIndex,
      int newVersion,
      IMigrationStore store,
      Func<CreateIndexDescriptor, ICreateIndexRequest>? creator = null,
      bool clean = false)
  {
    if ((oldIndex.Version ?? 0) < newVersion &&
        store.Processors.TryGetValue(
          oldIndex.Base,
          out var indexProcessors))
    {
      var newIndex = oldIndex.WithVersion(newVersion);
      Log.LogInformation(
          "Migrating {OldIndex} to {NewIndex}",
          oldIndex.Name,
          newIndex.Name);

      await client.Ingest
        .PutPipelineAsync(new Id(newIndex.Name), p => p
          .Processors(indexProcessors));

      if (creator is not null)
      {
        await client.Indices.CreateAsync(newIndex.Name, creator);
      }

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
      Func<CreateIndexDescriptor, ICreateIndexRequest>? creator = null,
      bool clean = false)
  {
    if ((oldIndex.Version ?? 0) < newVersion &&
        store.Processors.TryGetValue(
          oldIndex.Base,
          out var indexProcessors))
    {
      var newIndex = oldIndex.WithVersion(newVersion);
      Log.LogInformation(
          "Migrating {OldIndex} to {NewIndex}",
          oldIndex.Name,
          newIndex.Name);

      client.Ingest
        .PutPipeline(new Id(newIndex.Name), p => p
          .Processors(indexProcessors));

      if (creator is not null)
      {
        client.Indices.Create(newIndex.Name, creator);
      }

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

  private IEnumerable<(int Version, IMigrationStore Store)>
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
      .OrderBy(versionAndStore => versionAndStore.version)
      .With(migrations => Log
        .LogDebug(
          "Discovered {Migrations}",
          migrations.Select(migration => migration.version)));

  private Task<Indices>
  DiscoverIndicesAsync(
      IElasticClient client,
      bool isDev,
      int? version = null) =>
    client.Cat
      .IndicesAsync(i => i
        .Index(Nest.Indices.Parse($"{Index.MakePrefix(isDev)}*")))
      .Then(indices =>
        new Indices(
          indices.Records
            .Select(record => record.Index)
            .Where(index => !Index.IsTest(index)),
          isDev,
          version))
      .ThenWith(indices => Log
        .LogDebug(
          "Discovered {Indices}",
          indices.Yield().Select(index => index.Name)));

  private Indices
  DiscoverIndices(
      IElasticClient client,
      bool isDev,
      int? version = null) =>
    client.Cat
      .Indices(i => i
        .Index(Nest.Indices.Parse($"{Index.MakePrefix(isDev)}*")))
      .Var(indices =>
        new Indices(
          indices.Records
            .Select(record => record.Index)
            .Where(index => !Index.IsTest(index)),
          isDev,
          version))
      .With(indices => Log
        .LogDebug(
          "Discovered {Indices}",
          indices.Yield().Select(index => index.Name)));
}
