using Nest;
using OpType = Elasticsearch.Net.OpType;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<IndexResponse> CreateLoadLogAsync(LoadLog log);

  public IndexResponse CreateLoadLog(LoadLog log);

  public Task<IndexResponse> CreateMissingDataLogAsync(MissingDataLog log);

  public IndexResponse CreateMissingDataLog(MissingDataLog log);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<IndexResponse> CreateLoadLogAsync(LoadLog log) =>
    Elastic
      .IndexAsync(log, s => s
        .OpType(OpType.Create)
        .Index(LogIndexName));

  public IndexResponse CreateLoadLog(LoadLog log) =>
    Elastic
      .Index(log, s => s
        .OpType(OpType.Create)
        .Index(LogIndexName));

  public Task<IndexResponse> CreateMissingDataLogAsync(MissingDataLog log) =>
    Elastic
      .IndexAsync(log, s => s
        .OpType(OpType.Create)
        .Index(LogIndexName));

  public IndexResponse CreateMissingDataLog(MissingDataLog log) =>
    Elastic
      .Index(log, s => s
        .OpType(OpType.Create)
        .Index(LogIndexName));
}
