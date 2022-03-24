using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public BulkResponse IndexDevices(IEnumerable<Device> devices);
  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices);
};

public sealed partial class Client : IClient {
  public BulkResponse IndexDevices(IEnumerable<Device> devices) =>
      this._client.Bulk(s => s.IndexMany(devices));

  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices) =>
      this._client.BulkAsync(s => s.IndexMany(devices));
}
}
