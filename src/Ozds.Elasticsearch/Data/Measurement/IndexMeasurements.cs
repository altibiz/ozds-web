using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> IndexMeasurementsAsync(
      IEnumerable<Measurement> measurements);

  public BulkResponse IndexMeasurements(
      IEnumerable<Measurement> measurements);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<BulkResponse> IndexMeasurementsAsync(
      IEnumerable<Measurement> measurements) =>
    Elastic.BulkAsync(s => s
      .IndexMany(measurements)
      .Index(MeasurementIndexName));

  public BulkResponse IndexMeasurements(
      IEnumerable<Measurement> measurements) =>
    Elastic.Bulk(s => s
      .IndexMany(measurements)
      .Index(MeasurementIndexName));
}
