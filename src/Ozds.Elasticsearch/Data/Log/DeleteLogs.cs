using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public BulkResponse DeleteLogs(IEnumerable<Id> logIds);
  public Task<BulkResponse> DeleteLogsAsync(IEnumerable<Id> logIds);
};

public sealed partial class Client : IClient
{
  public BulkResponse
  DeleteLogs(IEnumerable<Id> logIds) => this.Elasticsearch.Bulk(
      s => s.DeleteMany<Log>(logIds.ToStrings()).Index(LogIndexName));

  public Task<BulkResponse>
  DeleteLogsAsync(IEnumerable<Id> logIds) => this.Elasticsearch.BulkAsync(
      s => s.DeleteMany<Log>(logIds.ToStrings()).Index(LogIndexName));
}
