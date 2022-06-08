using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement);

  public IndexResponse IndexMeasurement(Measurement measurement);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public IndexResponse IndexMeasurement(Measurement measurement) =>
    Elasticsearch.Index(measurement, s => s
      .Index(MeasurementIndexName));

  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement) =>
    Elasticsearch.IndexAsync(measurement, s => s
      .Index(MeasurementIndexName));
}
