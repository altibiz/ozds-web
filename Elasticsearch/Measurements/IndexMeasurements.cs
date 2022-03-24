using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public BulkResponse IndexMeasurements(IEnumerable<Measurement> measurements);
  public Task<BulkResponse> IndexMeasurementsAsync(
      IEnumerable<Measurement> measurements);
};

public sealed partial class Client : IClient {
  public BulkResponse
  IndexMeasurements(IEnumerable<Measurement> measurements) => this._client.Bulk(
      s => s.IndexMany(measurements));

  public async Task<BulkResponse> IndexMeasurementsAsync(
      IEnumerable<Measurement> measurements) =>
      await this._client.BulkAsync(s => s.IndexMany(measurements));
}
}
