using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<GetResponse<Measurement>> GetMeasurementAsync(Id id);

  public GetResponse<Measurement> GetMeasurement(Id id);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<GetResponse<Measurement>> GetMeasurementAsync(Id id) =>
    Elasticsearch
      .GetAsync<Measurement>(id, d => d
        .Index(MeasurementIndexName));

  public GetResponse<Measurement> GetMeasurement(Id id) =>
    Elasticsearch
      .Get<Measurement>(id, d => d
        .Index(MeasurementIndexName));
}
