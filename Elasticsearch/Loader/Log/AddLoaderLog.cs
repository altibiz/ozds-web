using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public void AddLoaderLog(Loader.Log log);
  public Task AddLoaderLogAsync(Loader.Log log);
};

public sealed partial class Client : IClient {
  public void AddLoaderLog(Loader.Log log) => this._client.Index(
      log, s => s.Index(Client.LoaderLogIndexName));

  public async Task AddLoaderLogAsync(
      Loader.Log log) => await this._client.IndexAsync(log,
      s => s.Index(Client.LoaderLogIndexName));
}
}
