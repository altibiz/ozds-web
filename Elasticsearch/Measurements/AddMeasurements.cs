using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public void addMeasurements(IEnumerable<Measurement> measurements);
  public Task addMeasurementsAsync(IEnumerable<Measurement> measurements);
};

public sealed partial class Client : IClient {
  public void
  addMeasurements(IEnumerable<Measurement> measurements) => this._client.Bulk(
      s => s.Index(Client.MeasurementsIndexName).IndexMany(measurements));

  public async Task addMeasurementsAsync(
      IEnumerable<Measurement> measurements) =>
      await this._client.BulkAsync(
          s => s.Index(Client.MeasurementsIndexName).IndexMany(measurements));
}
}
