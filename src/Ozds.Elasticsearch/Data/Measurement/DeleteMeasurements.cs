using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<BulkResponse> DeleteMeasurementsAsync(
      IEnumerable<Id> measurementIds);

  public BulkResponse DeleteMeasurements(
      IEnumerable<Id> measurementIds);
};

public sealed partial class Client : IClient
{
  public BulkResponse DeleteMeasurements(
      IEnumerable<Id> measurementIds) =>
    Elasticsearch.Bulk(s => s
      .DeleteMany<Measurement>(measurementIds.ToStrings())
      .Index(MeasurementIndexName));

  public Task<BulkResponse> DeleteMeasurementsAsync(
      IEnumerable<Id> measurementIds) =>
    Elasticsearch.BulkAsync(s => s
      .DeleteMany<Measurement>(measurementIds.ToStrings())
      .Index(MeasurementIndexName));
}
