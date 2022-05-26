using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement);

  public IndexResponse IndexMeasurement(Measurement measurement);
};

public sealed partial class Client : IClient
{
  public IndexResponse IndexMeasurement(Measurement measurement) =>
    Elasticsearch.Index(measurement, s => s
      .Index(MeasurementIndexName));

  public Task<IndexResponse> IndexMeasurementAsync(Measurement measurement) =>
    Elasticsearch.IndexAsync(measurement, s => s
      .Index(MeasurementIndexName));
}
