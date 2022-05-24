using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public GetResponse<Measurement> GetMeasurement(Id id);

  public Task<GetResponse<Measurement>> GetMeasurementAsync(Id id);
};

public sealed partial class Client : IClient
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
