using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> DeleteLogsAsync(IEnumerable<Id> logIds);

  public BulkResponse DeleteLogs(IEnumerable<Id> logIds);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<BulkResponse>
  DeleteLogsAsync(IEnumerable<Id> logIds) =>
    Elastic.BulkAsync(s => s
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/3500
      // NOTE: you would have to pass the object type here but we don't want to
      // NOTE: write multiple functions just because of the type
      .DeleteMany(logIds.Select(id => new { Id = id }))
      .RefreshInDevelopment(Env)
      .Index(LogIndexName));

  public BulkResponse
  DeleteLogs(IEnumerable<Id> logIds) =>
    Elastic.Bulk(s => s
      // NOTE: https://github.com/elastic/elasticsearch-net/issues/3500
      // NOTE: you would have to pass the object type here but we don't want to
      // NOTE: write multiple functions just because of the type
      .DeleteMany(logIds.Select(id => new { Id = id }))
      .RefreshInDevelopment(Env)
      .Index(LogIndexName));
}
