using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IndexResponse IndexMeasurement(Measurement measurement);
  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement);
};

public sealed partial class Client : IClient {
  public IndexResponse IndexMeasurement(
      Measurement measurement) => this._client.Index(measurement, s => s);

  public Task<IndexResponse> IndexMeasurementAsync(
      Measurement measurement) => this._client.IndexAsync(measurement, s => s);
}
}
