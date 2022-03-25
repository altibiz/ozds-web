using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public BulkResponse DeleteMeasurements(IEnumerable<Id> measurementIds);
  public Task<BulkResponse> DeleteMeasurementsAsync(
      IEnumerable<Id> measurementIds);
};

public sealed partial class Client : IClient {
  public BulkResponse
  DeleteMeasurements(IEnumerable<Id> measurementIds) => this._client.Bulk(
      s => s.DeleteMany<Measurement>(measurementIds.ToStrings()));

  public Task<BulkResponse> DeleteMeasurementsAsync(
      IEnumerable<Id> measurementIds) =>
      this._client.BulkAsync(
          s => s.DeleteMany<Measurement>(measurementIds.ToStrings()));
}
}
