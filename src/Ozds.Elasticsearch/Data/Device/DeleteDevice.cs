using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<DeleteResponse> DeleteDeviceAsync(Id id);

  public DeleteResponse DeleteDevice(Id id);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<DeleteResponse> DeleteDeviceAsync(Id id) =>
    Elastic.DeleteAsync(
      DocumentPath<Device>.Id(id),
      s => s.Index(DeviceIndexName));

  public DeleteResponse DeleteDevice(Id id) =>
    Elastic.Delete(
      DocumentPath<Device>.Id(id),
      s => s.Index(DeviceIndexName));
}
