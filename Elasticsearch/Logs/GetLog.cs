using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IGetResponse<Log> GetLoaderLog(Id id);

  public Task<IGetResponse<Log>> GetLoaderLogAsync(Id id);
};

public sealed partial class Client : IClient {
  public IGetResponse<Log> GetLoaderLog(
      Id id) => _client.Get<Log>(id);

  public async Task<IGetResponse<Log>> GetLoaderLogAsync(
      Id id) => await _client.GetAsync<Log>(id);
}
}
