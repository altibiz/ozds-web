using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IndexResponse IndexLoaderLog(Loader.Log log);
  public Task<IndexResponse> IndexLoaderLogAsync(Loader.Log log);
};

public sealed partial class Client : IClient {
  public IndexResponse IndexLoaderLog(Loader.Log log) => this._client.Index(
      log, s => s);

  public async Task<IndexResponse> IndexLoaderLogAsync(
      Loader.Log log) => await this._client.IndexAsync(log, s => s);
}
}
