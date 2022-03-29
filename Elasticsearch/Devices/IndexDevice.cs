using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IndexResponse IndexDevice(Device device);
  public Task<IndexResponse> IndexDeviceAsync(Device device);
};

public sealed partial class Client : IClient {
  public IndexResponse IndexDevice(Device device) => this.Elasticsearch.Index(
      device, s => s.Index(DeviceIndexName));

  public Task<IndexResponse> IndexDeviceAsync(
      Device device) => this.Elasticsearch.IndexAsync(device,
      s => s.Index(DeviceIndexName));
}
}
