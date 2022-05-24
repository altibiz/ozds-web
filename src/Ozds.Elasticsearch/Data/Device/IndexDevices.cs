using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public BulkResponse IndexDevices(IEnumerable<Device> devices);

  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices);
};

public sealed partial class Client : IClient
{
  public BulkResponse IndexDevices(IEnumerable<Device> devices) =>
    Elasticsearch
      .Bulk(s => s
        .IndexMany(devices)
        .Index(DeviceIndexName));

  public Task<BulkResponse> IndexDevicesAsync(IEnumerable<Device> devices) =>
    Elasticsearch
      .BulkAsync(s => s
        .IndexMany(devices)
        .Index(DeviceIndexName));
}
