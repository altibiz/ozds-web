using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public interface IElasticsearchMigratorAccessor
{
  public IElasticsearchMigrator? Migrator { get; }
}

public class ElasticsearchMigratorAccessor : IElasticsearchMigratorAccessor
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
  public Task<Indices>
  MigrateAsync(
      IElasticClient client,
      bool clean = false);

  public Indices
  Migrate(
      IElasticClient client,
      bool clean = false);
}

public class ElasticsearchMigrator : IElasticsearchMigrator
{
  public Task<Indices>
  MigrateAsync(
      IElasticClient client,
      bool clean = false) =>
    DiscoverIndicesAsync(client, Env.IsDevelopment())
      .ThenAwait(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Var(migrations => migrations
          .Select((migration, index) => oldIndices
            .Yield()
            .Select(oldIndex =>
              (Old: oldIndex,
               New: oldIndex.WithVersion(migration.Version)))
            .Select(indices =>
                index == (migrations.Count() - 1) ?
                MigrateAsync(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  creator: Index.AsyncCreatorFor[indices.Old.Base],
                  mapper: Index.AsyncMapperFor[indices.Old.Base],
                  clean: clean)
              : MigrateAsync(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  clean: clean))
            .AwaitValue()
            .Then(IndexExtensions.Indices))
          .AwaitValue()
          .Then(indices => indices
            .LastOrDefault())));

  public Indices
  Migrate(
      IElasticClient client,
      bool clean = false) =>
    DiscoverIndices(client, Env.IsDevelopment())
      .Var(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Var(migrations => migrations
          .Select((migration, index) => oldIndices
            .Yield()
            .Select(oldIndex =>
              (Old: oldIndex,
               New: oldIndex.WithVersion(migration.Version)))
            .Select(indices =>
                index == (migrations.Count() - 1) ?
                Migrate(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  creator: Index.CreatorFor[indices.Old.Base],
                  mapper: Index.MapperFor[indices.Old.Base],
                  clean: clean)
              : Migrate(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  clean: clean))
            .Var(IndexExtensions.Indices))
          .Var(indices => indices
            .LastOrDefault())));

  private async Task<Index> MigrateAsync(
      IElasticClient client,
      Index oldIndex,
      Index newIndex,
      IMigrationStore store,
      Func<IElasticClient, Index, Task<CreateIndexResponse>>? creator = null,
      Func<IElasticClient, Index, Task<PutMappingResponse>>? mapper = null,
      bool clean = false) =>
    oldIndex switch
    {
      _ when (oldIndex.Version ?? 0) >= newIndex.Version => oldIndex,
      _ =>
          creator is not null &&
          await creator(client, newIndex) is
          { IsValid: false } createIndexResponse ?
          throw new InvalidOperationException(
            $"Failed creating index {newIndex.Name}",
            createIndexResponse.OriginalException)
        : mapper is not null &&
          await mapper(client, newIndex) is
          { IsValid: false } mapIndexResponse ?
          throw new InvalidOperationException(
            $"Failed mapping index {newIndex}",
            mapIndexResponse.OriginalException)
        : store.Processors.GetOrDefault(oldIndex.Base) switch
        {
          null or { Count: 0 } =>
              await client
                .ReindexOnServerAsync(r => r
                  .WaitForCompletion()
                  .Source(s => s
                    .Index(oldIndex.Name))
                  .Destination(d => d
                    .Index(newIndex.Name))) is
              { IsValid: false } reindexResponse ?
              throw new InvalidOperationException(
                $"Failed reindexing {newIndex.Name}",
                reindexResponse.OriginalException)
            : clean switch
            {
              false => newIndex,
              true =>
                    await client.Indices
                      .DeleteAsync(oldIndex.Name) is
                    { IsValid: false } deleteIndexResponse ?
                    throw new InvalidOperationException(
                      $"Failed deleting index {newIndex.Name}",
                      deleteIndexResponse.OriginalException)
                  : newIndex,
            },
          var processors =>
              await client.Ingest
                .PutPipelineAsync(new Id(newIndex.Name), p => p
                  .Processors(processors)) is
              { IsValid: false } putPipelineResponse ?
              throw new InvalidOperationException(
                $"Failed creating pipeline {newIndex.Name}",
                putPipelineResponse.OriginalException)
            : await client
                .ReindexOnServerAsync(r => r
                  .WaitForCompletion()
                  .Source(s => s
                    .Index(oldIndex.Name))
                  .Destination(d => d
                    .Index(newIndex.Name)
                    .Pipeline(newIndex.Name))) is
            { IsValid: false } reindexResponse ?
              throw new InvalidOperationException(
                  $"Failed reindexing {newIndex.Name}",
                  reindexResponse.OriginalException)
            : clean switch
            {
              false => newIndex,
              true =>
                    await client.Ingest
                      .DeletePipelineAsync(new Id(newIndex.Name)) is
                    { IsValid: false } deletePipelineResponse ?
                    throw new InvalidOperationException(
                      $"Failed deleting pipeline {newIndex.Name}",
                      deletePipelineResponse.OriginalException)
                  : await client.Indices
                      .DeleteAsync(oldIndex.Name) is
                  { IsValid: false } deleteIndexResponse ?
                    throw new InvalidOperationException(
                      $"Failed deleting index {newIndex.Name}",
                      deleteIndexResponse.OriginalException)
                  : newIndex,
            }
        }
    };

  private Index Migrate(
      IElasticClient client,
      Index oldIndex,
      Index newIndex,
      IMigrationStore store,
      Func<IElasticClient, Index, CreateIndexResponse>? creator = null,
      Func<IElasticClient, Index, PutMappingResponse>? mapper = null,
      bool clean = false) =>
    oldIndex switch
    {
      _ when (oldIndex.Version ?? 0) >= newIndex.Version => oldIndex,
      _ =>
          creator is not null &&
          creator(client, newIndex) is
          { IsValid: false } createIndexResponse ?
          throw new InvalidOperationException(
            $"Failed mapping {newIndex.Name}",
            createIndexResponse.OriginalException)
        : mapper is not null &&
          mapper(client, newIndex) is
          { IsValid: false } mapIndexResponse ?
          throw new InvalidOperationException(
            $"Failed mapping {newIndex}",
            mapIndexResponse.OriginalException)
        : store.Processors.GetOrDefault(oldIndex.Base) switch
        {
          null or { Count: 0 } =>
              client
                .ReindexOnServer(r => r
                  .WaitForCompletion()
                  .Source(s => s
                    .Index(oldIndex.Name))
                  .Destination(d => d
                    .Index(newIndex.Name))) is
              { IsValid: false } reindexResponse ?
              throw new InvalidOperationException(
                $"Failed reindexing {newIndex.Name}",
                reindexResponse.OriginalException)
            : clean switch
            {
              false => newIndex,
              true =>
                    client.Indices
                      .Delete(oldIndex.Name) is
                    { IsValid: false } deleteIndexResponse ?
                    throw new InvalidOperationException(
                      $"Failed deleting index {newIndex.Name}",
                      deleteIndexResponse.OriginalException)
                  : newIndex,
            },
          var processors =>
              client.Ingest
                .PutPipeline(new Id(newIndex.Name), p => p
                  .Processors(processors)) is
              { IsValid: false } putPipelineResponse ?
              throw new InvalidOperationException(
                $"Failed creating pipeline {newIndex.Name}",
                putPipelineResponse.OriginalException)
            : client
                .ReindexOnServer(r => r
                  .WaitForCompletion()
                  .Source(s => s
                    .Index(oldIndex.Name))
                  .Destination(d => d
                    .Index(newIndex.Name)
                    .Pipeline(newIndex.Name))) is
            { IsValid: false } reindexReponse ?
              throw new InvalidOperationException(
                $"Failed reindexing {newIndex.Name}",
                reindexReponse.OriginalException)
            : clean switch
            {
              false => newIndex,
              true =>
                    client.Ingest
                      .DeletePipeline(new Id(newIndex.Name)) is
                    { IsValid: false } deletePipelineResponse ?
                    throw new InvalidOperationException(
                      $"Failed deleting pipeline {newIndex.Name}",
                      deletePipelineResponse.OriginalException)
                  : client.Indices
                      .Delete(oldIndex.Name) is
                  { IsValid: false } deleteIndexResponse ?
                      throw new InvalidOperationException(
                        $"Failed deleting index {newIndex.Name}",
                        deleteIndexResponse.OriginalException)
                  : newIndex,
            }
        }
    };

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
          "Discovered migrations {Migrations}",
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
          "Discovered indices {Indices}",
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
          "Discovered indices {Indices}",
          indices.Yield().Select(index => index.Name)));
}
