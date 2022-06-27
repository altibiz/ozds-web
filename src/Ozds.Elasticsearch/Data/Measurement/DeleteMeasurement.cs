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
    Elastic.DeleteAsync(DocumentPath<Measurement>.Id(id), s => s
      .RefreshInDevelopment(Env)
      .Index(MeasurementIndexName));

  public DeleteResponse DeleteMeasurement(Id id) =>
    Elastic.Delete(DocumentPath<Measurement>.Id(id), s => s
      .RefreshInDevelopment(Env)
      .Index(MeasurementIndexName));
}
