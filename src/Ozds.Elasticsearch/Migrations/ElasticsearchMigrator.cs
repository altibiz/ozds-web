using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public class ElasticsearchMigrator : IElasticsearchMigrator
{
  public Task<IIndices>
  MigrateAsync(
      IElasticClient client,
      bool clean = false) =>
    DiscoverIndicesAsync(client)
      .ThenAwait(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Var(migrations => migrations
          .Select((migration, index) => oldIndices
            .Select(oldIndex =>
              (Old: oldIndex,
               New: Namer.WithVersion(oldIndex, migration.Version)))
            .Select(indices =>
                index == (migrations.Count() - 1) ?
                MigrateAsync(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  creator: Mapper.AsyncCreatorFor[indices.New.Base],
                  mapper: Mapper.AsyncMapperFor[indices.New.Base],
                  clean: clean)
              : MigrateAsync(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  clean: clean))
            .AwaitValue()
            .Then(Namer.MakeIndices))
          .AwaitValue()
          .Then(indices => indices
            .LastOrDefault(Namer
              .MakeIndices()))));

  public IIndices
  Migrate(
      IElasticClient client,
      bool clean = false) =>
    DiscoverIndices(client)
      .Var(oldIndices => DiscoverMigrations(typeof(Migrations))
        .Var(migrations => migrations
          .Select((migration, index) => oldIndices
            .Select(oldIndex =>
              (Old: oldIndex,
               New: Namer.WithVersion(oldIndex, migration.Version)))
            .Select(indices =>
                index == (migrations.Count() - 1) ?
                Migrate(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  creator: Mapper.CreatorFor[indices.New.Base],
                  mapper: Mapper.MapperFor[indices.New.Base],
                  clean: clean)
              : Migrate(
                  client: client,
                  oldIndex: indices.Old,
                  newIndex: indices.New,
                  store: migration.Store,
                  clean: clean))
            .Var(Namer.MakeIndices))
          .Var(indices => indices
            .LastOrDefault(Namer
              .MakeIndices()))));

  private async Task<IIndex> MigrateAsync(
      IElasticClient client,
      IIndex oldIndex,
      IIndex newIndex,
      IMigrationStore store,
      Func<IElasticClient, IIndex, Task<CreateIndexResponse>>? creator = null,
      Func<IElasticClient, IIndex, Task<PutMappingResponse>>? mapper = null,
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
                  .WaitForCompletion(false)
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
                  .WaitForCompletion(false)
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

  private IIndex Migrate(
      IElasticClient client,
      IIndex oldIndex,
      IIndex newIndex,
      IMigrationStore store,
      Func<IElasticClient, IIndex, CreateIndexResponse>? creator = null,
      Func<IElasticClient, IIndex, PutMappingResponse>? mapper = null,
      bool clean = false) =>
    oldIndex switch
    {
      _ when (oldIndex.Version ?? 0) >= newIndex.Version => oldIndex,
      _ =>
          creator is not null &&
          creator(client, newIndex) is
            { IsValid: false } createIndexResponse ?
          throw new InvalidOperationException(
            $"Failed creating {newIndex.Name}",
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
                    .WaitForCompletion(false)
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
                    .WaitForCompletion(false)
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

  // TODO: don't use cat API
  private Task<IIndices>
  DiscoverIndicesAsync(
      IElasticClient client,
      int? version = null) =>
    client.Cat
      .IndicesAsync(i => i
        .Index(Nest.Indices.Parse($"{Namer.MakePrefix()}*")))
      .Then(indices => Namer
        .MakeIndices(
          indices.Records
            .Select(record => record.Index)
            .Where(index => !Namer.IsTest(index)),
          version))
      .ThenWith(indices => Log
        .LogDebug(
          "Discovered indices {Indices}",
          indices.Yield().Select(index => index.Name)));

  // TODO: don't use cat API
  private IIndices
  DiscoverIndices(
      IElasticClient client,
      int? version = null) =>
    client.Cat
      .Indices(i => i
        .Index(Nest.Indices.Parse($"{Namer.MakePrefix()}*")))
      .Var(indices => Namer
        .MakeIndices(
          indices.Records
            .Select(record => record.Index)
            .Where(index => !Namer.IsTest(index)),
          version))
      .With(indices => Log
        .LogDebug(
          "Discovered indices {Indices}",
          indices.Yield().Select(index => index.Name)));

  public ElasticsearchMigrator(
      ILogger<ElasticsearchMigrator> log,
      IHostEnvironment env,

      IIndexNamer namer,
      IIndexMapper mapper)
  {
    Log = log;
    Env = env;

    Namer = namer;
    Mapper = mapper;
  }

  private ILogger Log { get; }
  private IHostEnvironment Env { get; }

  private IIndexNamer Namer { get; }
  private IIndexMapper Mapper { get; }
}
