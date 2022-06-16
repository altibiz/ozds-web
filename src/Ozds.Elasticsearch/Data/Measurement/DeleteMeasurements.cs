using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> DeleteMeasurementsAsync(
      IEnumerable<Id> measurementIds);

  public BulkResponse DeleteMeasurements(
      IEnumerable<Id> measurementIds);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public BulkResponse DeleteMeasurements(
      IEnumerable<Id> measurementIds) =>
    Elastic.Bulk(s => s
      .DeleteMany<Measurement>(measurementIds.ToStrings())
      .Index(MeasurementIndexName));

  public Task<BulkResponse> DeleteMeasurementsAsync(
      IEnumerable<Id> measurementIds) =>
    Elastic.BulkAsync(s => s
      .DeleteMany<Measurement>(measurementIds.ToStrings())
      .Index(MeasurementIndexName));
}
