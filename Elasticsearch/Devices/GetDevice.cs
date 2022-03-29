using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IGetResponse<Device> GetDevice(Id id);

  public Task<IGetResponse<Device>> GetDeviceAsync(Id id);
};

public sealed partial class Client : IClient {
  public IGetResponse<Device> GetDevice(Id id) => Elasticsearch.Get<Device>(
      id, s => s.Index(DeviceIndexName));

  public async Task<IGetResponse<Device>> GetDeviceAsync(
      Id id) => await Elasticsearch.GetAsync<Device>(id,
      s => s.Index(DeviceIndexName));
}
}
