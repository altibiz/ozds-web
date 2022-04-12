using System.Threading.Tasks;
using Nest;

namespace Ozds.Elasticsearch
{
  public partial interface IClient
  {
    public DeleteResponse DeleteDevice(Id id);
    public Task<DeleteResponse> DeleteDeviceAsync(Id id);
  };

  public sealed partial class Client : IClient
  {
    public DeleteResponse DeleteDevice(Id id) => this.Elasticsearch.Delete(
        DocumentPath<Device>.Id(id), s => s.Index(DeviceIndexName));

    public async Task<DeleteResponse>
    DeleteDeviceAsync(Id id) => await this.Elasticsearch.DeleteAsync(
        DocumentPath<Device>.Id(id), s => s.Index(DeviceIndexName));
  }
}
