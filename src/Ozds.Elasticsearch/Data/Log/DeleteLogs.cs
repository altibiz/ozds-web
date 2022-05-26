using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<BulkResponse> DeleteLogsAsync(IEnumerable<Id> logIds);

  public BulkResponse DeleteLogs(IEnumerable<Id> logIds);
};

public sealed partial class Client : IClient
{
  public Task<BulkResponse>
  DeleteLogsAsync(IEnumerable<Id> logIds) =>
    Elasticsearch.BulkAsync(s => s
      .DeleteMany(logIds.ToStrings())
      .Index(LogIndexName));

  public BulkResponse
  DeleteLogs(IEnumerable<Id> logIds) =>
    Elasticsearch.Bulk(s => s
      .DeleteMany(logIds.ToStrings())
      .Index(LogIndexName));
}
