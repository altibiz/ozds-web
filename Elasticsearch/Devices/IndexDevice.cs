using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IndexResponse IndexDevice(Device device);
  public Task<IndexResponse> IndexDeviceAsync(Device device);
};

public sealed partial class Client : IClient {
  public IndexResponse IndexDevice(Device device) => this._client.Index(
      device, s => s);

  public Task<IndexResponse> IndexDeviceAsync(
      Device device) => this._client.IndexAsync(device,
      s => s);
}
}
