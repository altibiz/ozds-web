using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public DeleteResponse DeleteLoaderLog(Id id);
  public Task<DeleteResponse> DeleteLoaderLogAsync(Id id);
};

public sealed partial class Client : IClient {
  public DeleteResponse DeleteLoaderLog(Id id) => this._client.Delete(
      DocumentPath<Loader.Log>.Id(id));

  public async Task<DeleteResponse> DeleteLoaderLogAsync(
      Id id) => await this._client.DeleteAsync(DocumentPath<Loader.Log>.Id(id));
}
}
