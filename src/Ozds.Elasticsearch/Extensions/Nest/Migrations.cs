using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public static class MigrationExtensions
{
  public static Task<IElasticClient> MigrateAsync(
      this IElasticClient client,
      string prev,
      string next,
      string description = "",
      IEnumerable<IProcessor>? processors = null) =>
    client.Ingest
      .PutPipelineAsync(
        new Id(next),
        p => p
          .Description(description)
          .Processors(processors ?? new IProcessor[0]))
      .ThenAwait(async response =>
        !response.IsValid ?
          throw new InvalidOperationException(
            $"Failed migration pipeline creation from {prev} to {next}")
        : await client
            .ReindexOnServerAsync(r => r
              .WaitForCompletion()
              .Source(s => s
                .Index(prev))
              .Destination(d => d
                .Index(next)
                .Pipeline(next))))
      .Then(response =>
        !response.IsValid ?
          throw new InvalidOperationException(
            $"Failed migration reindexing from {prev} to {next}")
        : client);

  public static Task<IElasticClient> MigrateAsync(
      this Task<IElasticClient> client,
      string prev,
      string next,
      string description = "",
      IEnumerable<IProcessor>? processors = null) =>
    client
      .ThenAwait(client => client
        .MigrateAsync(
          prev: prev,
          next: next,
          description: description,
          processors: processors)
        .ToValueTask());
}
