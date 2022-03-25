using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public DeleteResponse DeleteDevice(Id id);
  public Task<DeleteResponse> DeleteDeviceAsync(Id id);
};

public sealed partial class Client : IClient {
  public DeleteResponse DeleteDevice(Id id) => this._client.Delete(
      DocumentPath<Device>.Id(id));

  public async Task<DeleteResponse> DeleteDeviceAsync(
      Id id) => await this._client.DeleteAsync(DocumentPath<Device>.Id(id));
}
}
