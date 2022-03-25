using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public DeleteResponse DeleteMeasurement(Id id);
  public Task<DeleteResponse> DeleteMeasurementAsync(Id id);
};

public sealed partial class Client : IClient {
  public DeleteResponse DeleteMeasurement(Id id) => this._client.Delete(
      DocumentPath<Measurement>.Id(id));

  public async Task<DeleteResponse> DeleteMeasurementAsync(Id id) =>
      await this._client.DeleteAsync(DocumentPath<Measurement>.Id(id));
}
}
