using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<DeleteResponse> DeleteMeasurementAsync(Id id);

  public DeleteResponse DeleteMeasurement(Id id);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<DeleteResponse>
  DeleteMeasurementAsync(Id id) =>
    Elasticsearch.DeleteAsync(
      DocumentPath<Measurement>.Id(id),
      s => s.Index(MeasurementIndexName));

  public DeleteResponse DeleteMeasurement(Id id) =>
    Elasticsearch.Delete(
      DocumentPath<Measurement>.Id(id),
      s => s.Index(MeasurementIndexName));
}
