using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public void RemoveLoaderLog(Loader.Log log);
  public Task RemoveLoaderLogAsync(Loader.Log log);
};

public sealed partial class Client : IClient {
  public void RemoveLoaderLog(Loader.Log log) => this._client.Delete(
      new Nest.DocumentPath<Loader.Log>(log),
      s => s.Index(Client.LoaderLogIndexName));

  public async Task RemoveLoaderLogAsync(Loader.Log log) =>
      await this._client.DeleteAsync(new Nest.DocumentPath<Loader.Log>(log),
          s => s.Index(Client.LoaderLogIndexName));
}
}
