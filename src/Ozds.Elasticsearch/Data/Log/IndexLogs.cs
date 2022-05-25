using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public BulkResponse IndexLogs(IEnumerable<Log> logs);

  public Task<BulkResponse> IndexLogsAsync(IEnumerable<Log> logs);
};

public sealed partial class Client : IClient
{
  public BulkResponse IndexLogs(IEnumerable<Log> logs) =>
    Elasticsearch
      .Bulk(s => s
        .IndexMany(logs)
        .Index(LogIndexName));

  public Task<BulkResponse> IndexLogsAsync(IEnumerable<Log> logs) =>
    Elasticsearch
      .BulkAsync(s => s
        .IndexMany(logs)
        .Index(LogIndexName));
}
