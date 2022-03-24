using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IGetResponse<Loader.Log> GetLoaderLog(Id id);

  public Task<IGetResponse<Loader.Log>> GetLoaderLogAsync(Id id);
};

public sealed partial class Client : IClient {
  public IGetResponse<Loader.Log> GetLoaderLog(
      Id id) => _client.Get<Loader.Log>(id, g => g);

  public async Task<IGetResponse<Loader.Log>> GetLoaderLogAsync(
      Id id) => await _client.GetAsync<Loader.Log>(id, g => g);
}
}
