using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch
{
  public partial interface IClient
  {
    public BulkResponse DeleteMeasurements(IEnumerable<Id> measurementIds);
    public Task<BulkResponse> DeleteMeasurementsAsync(
        IEnumerable<Id> measurementIds);
  };

  public sealed partial class Client : IClient
  {
    public BulkResponse
    DeleteMeasurements(IEnumerable<Id> measurementIds) => this.Elasticsearch.Bulk(
        s => s.DeleteMany<Measurement>(measurementIds.ToStrings())
                 .Index(MeasurementIndexName));

    public Task<BulkResponse> DeleteMeasurementsAsync(
        IEnumerable<Id> measurementIds) =>
        this.Elasticsearch.BulkAsync(
            s => s.DeleteMany<Measurement>(measurementIds.ToStrings())
                     .Index(MeasurementIndexName));
  }
}
