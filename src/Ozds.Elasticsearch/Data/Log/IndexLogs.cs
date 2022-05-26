using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
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

public sealed partial class Client : IClient
{
  public Task<BulkResponse> IndexLoadLogsAsync(
      IEnumerable<LoadLog> logs) =>
    Elasticsearch
      .BulkAsync(s => s
        .IndexMany(logs)
        .Index(LogIndexName));

  public BulkResponse IndexLoadLogs(
      IEnumerable<LoadLog> logs) =>
    Elasticsearch
      .Bulk(s => s
        .IndexMany(logs)
        .Index(LogIndexName));

  public Task<BulkResponse> IndexMissingDataLogsAsync(
      IEnumerable<MissingDataLog> logs) =>
    Elasticsearch
      .BulkAsync(s => s
        .IndexMany(logs)
        .Index(LogIndexName));

  public BulkResponse IndexMissingDataLogs(
      IEnumerable<MissingDataLog> logs) =>
    Elasticsearch
      .Bulk(s => s
        .IndexMany(logs)
        .Index(LogIndexName));
}
