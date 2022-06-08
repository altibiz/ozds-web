using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> CreateLoadLogsAsync(
      IEnumerable<LoadLog> logs);

  public BulkResponse CreateLoadLogs(
      IEnumerable<LoadLog> logs);

  public Task<BulkResponse> CreateMissingDataLogsAsync(
      IEnumerable<MissingDataLog> logs);

  public BulkResponse CreateMissingDataLogs(
      IEnumerable<MissingDataLog> logs);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<BulkResponse> CreateLoadLogsAsync(
      IEnumerable<LoadLog> logs) =>
    Elasticsearch
      .BulkAsync(s => s
        .CreateMany(logs)
        .Index(LogIndexName));

  public BulkResponse CreateLoadLogs(
      IEnumerable<LoadLog> logs) =>
    Elasticsearch
      .Bulk(s => s
        .CreateMany(logs)
        .Index(LogIndexName));

  public Task<BulkResponse> CreateMissingDataLogsAsync(
      IEnumerable<MissingDataLog> logs) =>
    Elasticsearch
      .BulkAsync(s => s
        .CreateMany(logs)
        .Index(LogIndexName));

  public BulkResponse CreateMissingDataLogs(
      IEnumerable<MissingDataLog> logs) =>
    Elasticsearch
      .Bulk(s => s
        .CreateMany(logs)
        .Index(LogIndexName));
}
