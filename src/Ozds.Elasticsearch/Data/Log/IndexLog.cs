using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<IndexResponse> IndexLoadLogAsync(LoadLog log);

  public IndexResponse IndexLoadLog(LoadLog log);

  public Task<IndexResponse> IndexMissingDataLogAsync(MissingDataLog log);

  public IndexResponse IndexMissingDataLog(MissingDataLog log);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<IndexResponse> IndexLoadLogAsync(LoadLog log) =>
    Elastic
      .IndexAsync(log, s => s
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));

  public IndexResponse IndexLoadLog(LoadLog log) =>
    Elastic
      .Index(log, s => s
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));

  public Task<IndexResponse> IndexMissingDataLogAsync(MissingDataLog log) =>
    Elastic
      .IndexAsync(log, s => s
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));

  public IndexResponse IndexMissingDataLog(MissingDataLog log) =>
    Elastic
      .Index(log, s => s
        .RefreshInDevelopment(Env)
        .Index(LogIndexName));
}
