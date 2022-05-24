using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public DeleteResponse DeleteMeasurement(Id id);
  public Task<DeleteResponse> DeleteMeasurementAsync(Id id);
};

public sealed partial class Client : IClient
{
  public DeleteResponse DeleteMeasurement(Id id) => this.Elasticsearch.Delete(
      DocumentPath<Measurement>.Id(id).Index(MeasurementIndexName));

  public async Task<DeleteResponse>
  DeleteMeasurementAsync(Id id) => await this.Elasticsearch.DeleteAsync(
      DocumentPath<Measurement>.Id(id).Index(MeasurementIndexName));
}
