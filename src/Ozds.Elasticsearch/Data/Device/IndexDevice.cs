using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public IndexResponse IndexDevice(Device device);

  public Task<IndexResponse> IndexDeviceAsync(Device device);
};

public sealed partial class Client : IClient
{
  public IndexResponse IndexDevice(Device device) =>
    Elasticsearch
      .Index(
        device,
        s => s
          .Index(DeviceIndexName));

  public Task<IndexResponse> IndexDeviceAsync(Device device) =>
    Elasticsearch
      .IndexAsync(
        device,
        s => s
          .Index(DeviceIndexName));
}
