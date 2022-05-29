using Nest;
using Ozds.Extensions;

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
      s => s.Index(DeviceIndexName))
      .ThenWithTask(_ => DeleteLoadLogAsync(LoadLog
        .MakeId(id.ToString())));

  public DeleteResponse DeleteDevice(Id id) =>
    Elasticsearch.Delete(
      DocumentPath<Device>.Id(id),
      s => s.Index(DeviceIndexName))
      .WithNullable(_ => DeleteLoadLog(LoadLog
        .MakeId(id.ToString())));
}
