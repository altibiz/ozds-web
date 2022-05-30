using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds);

  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds);
};

public sealed partial class Client : IClient
{
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds) =>
    Elasticsearch.BulkAsync(s => s
      .DeleteMany<Device>(deviceIds.ToStrings())
      .Index(DeviceIndexName));

  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds) =>
    Elasticsearch.Bulk(s => s
      .DeleteMany<Device>(deviceIds.ToStrings())
      .Index(DeviceIndexName));
}
