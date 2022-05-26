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
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/3500
      // NOTE: you would have to pass the object type here but we don't want to
      // NOTE: write multiple functions just because of the type
      .DeleteMany(logIds.Select(id => new { Id = id }))
      .Index(LogIndexName));

  public BulkResponse
  DeleteLogs(IEnumerable<Id> logIds) =>
    Elasticsearch.Bulk(s => s
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/3500
      // NOTE: you would have to pass the object type here but we don't want to
      // NOTE: write multiple functions just because of the type
      .DeleteMany(logIds.Select(id => new { Id = id }))
      .Index(LogIndexName));
}
