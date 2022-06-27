using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> IndexLoadLogsAsync(
      IEnumerable<LoadLog> logs);

  public BulkResponse IndexLoadLogs(
      IEnumerable<LoadLog> logs);

  public Task<BulkResponse> IndexMissingDataLogsAsync(
      IEnumerable<MissingDataLog> logs);

  public BulkResponse IndexMissingDataLogs(
      IEnumerable<MissingDataLog> logs);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<BulkResponse> IndexLoadLogsAsync(
      IEnumerable<LoadLog> logs) =>
    Elastic
      .BulkAsync(s => s
        .IndexMany(logs)
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));

  public BulkResponse IndexLoadLogs(
      IEnumerable<LoadLog> logs) =>
    Elastic
      .Bulk(s => s
        .IndexMany(logs)
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));

  public Task<BulkResponse> IndexMissingDataLogsAsync(
      IEnumerable<MissingDataLog> logs) =>
    Elastic
      .BulkAsync(s => s
        .IndexMany(logs)
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));

  public BulkResponse IndexMissingDataLogs(
      IEnumerable<MissingDataLog> logs) =>
    Elastic
      .Bulk(s => s
        .IndexMany(logs)
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));
}
