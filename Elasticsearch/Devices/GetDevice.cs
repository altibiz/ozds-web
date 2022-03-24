using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IGetResponse<Device> GetDevice(Id id);

  public Task<IGetResponse<Device>> GetDeviceAsync(Id id);
};

public sealed partial class Client : IClient {
  public IGetResponse<Device> GetDevice(Id id) => _client.Get<Device>(id);

  public async Task<IGetResponse<Device>> GetDeviceAsync(
      Id id) => await _client.GetAsync<Device>(id);
}
}
