using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<DeleteResponse> DeleteDeviceAsync(Id id);

  public DeleteResponse DeleteDevice(Id id);
};

public sealed partial class Client : IClient
{
  public Task<DeleteResponse> DeleteDeviceAsync(Id id) =>
    Elasticsearch.DeleteAsync(
      DocumentPath<Device>.Id(id),
      s => s.Index(DeviceIndexName));

  public DeleteResponse DeleteDevice(Id id) =>
    Elasticsearch.Delete(
      DocumentPath<Device>.Id(id),
      s => s.Index(DeviceIndexName));
}
