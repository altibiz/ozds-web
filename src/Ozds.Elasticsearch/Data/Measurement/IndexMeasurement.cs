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
    Elastic.Index(measurement, s => s
      .RefreshInDevelopment(Env)
      .Index(MeasurementIndexName));

  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement) =>
    Elastic.IndexAsync(measurement, s => s
      .RefreshInDevelopment(Env)
      .Index(MeasurementIndexName));
}
