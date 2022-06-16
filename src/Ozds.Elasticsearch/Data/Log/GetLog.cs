using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<GetResponse<LoadLog>> GetLoadLogAsync(Id id);

  public GetResponse<LoadLog> GetLoadLog(Id id);

  public Task<GetResponse<MissingDataLog>> GetMissingDataLogAsync(Id id);

  public GetResponse<MissingDataLog> GetMissingDataLog(Id id);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<GetResponse<LoadLog>> GetLoadLogAsync(Id id) =>
    Elastic.GetAsync<LoadLog>(id, d => d.Index(LogIndexName));

  public GetResponse<LoadLog> GetLoadLog(Id id) =>
    Elastic.Get<LoadLog>(id, d => d.Index(LogIndexName));

  public Task<GetResponse<MissingDataLog>> GetMissingDataLogAsync(Id id) =>
    Elastic.GetAsync<MissingDataLog>(id, d => d.Index(LogIndexName));

  public GetResponse<MissingDataLog> GetMissingDataLog(Id id) =>
    Elastic.Get<MissingDataLog>(id, d => d.Index(LogIndexName));
}
