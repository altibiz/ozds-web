using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IGetResponse<Measurement> GetMeasurement(Id id);

  public Task<IGetResponse<Measurement>> GetMeasurementAsync(Id id);
};

public sealed partial class Client : IClient {
  public IGetResponse<Measurement> GetMeasurement(
      Id id) => _client.Get<Measurement>(id);

  public async Task<IGetResponse<Measurement>> GetMeasurementAsync(
      Id id) => await _client.GetAsync<Measurement>(id);
}
}
