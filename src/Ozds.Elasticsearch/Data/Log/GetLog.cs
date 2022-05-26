using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<GetResponse<LoadLog>> GetLoadLogAsync(Id id);

  public GetResponse<LoadLog> GetLoadLog(Id id);

  public Task<GetResponse<MissingDataLog>> GetMissingDataLogAsync(Id id);

  public GetResponse<MissingDataLog> GetMissingDataLog(Id id);
};

public sealed partial class Client : IClient
{
  public Task<GetResponse<LoadLog>> GetLoadLogAsync(Id id) =>
    Elasticsearch.GetAsync<LoadLog>(id, d => d.Index(LogIndexName));

  public GetResponse<LoadLog> GetLoadLog(Id id) =>
    Elasticsearch.Get<LoadLog>(id, d => d.Index(LogIndexName));

  public Task<GetResponse<MissingDataLog>> GetMissingDataLogAsync(Id id) =>
    Elasticsearch.GetAsync<MissingDataLog>(id, d => d.Index(LogIndexName));

  public GetResponse<MissingDataLog> GetMissingDataLog(Id id) =>
    Elasticsearch.Get<MissingDataLog>(id, d => d.Index(LogIndexName));
}
