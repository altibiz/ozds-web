using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds);
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds);
};

public sealed partial class Client : IClient {
  public BulkResponse
  DeleteDevices(IEnumerable<Id> deviceIds) => this.Elasticsearch.Bulk(
      s => s.DeleteMany<Device>(deviceIds.ToStrings()).Index(DeviceIndexName));

  public Task<BulkResponse>
  DeleteDevicesAsync(IEnumerable<Id> deviceIds) => this.Elasticsearch.BulkAsync(
      s => s.DeleteMany<Device>(deviceIds.ToStrings()).Index(DeviceIndexName));
}
}
