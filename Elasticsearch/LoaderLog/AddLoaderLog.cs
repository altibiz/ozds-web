using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public void addLoaderLog(LoaderLog log);
  public Task addLoaderLogAsync(LoaderLog log);
};

public sealed partial class Client : IClient {
  public void addLoaderLog(LoaderLog log) => this._client.Index(
      log, s => s.Index(Client.LoaderLogIndexName));

  public async Task addLoaderLogAsync(
      LoaderLog log) => await this._client.IndexAsync(log,
      s => s.Index(Client.LoaderLogIndexName));
}
}
