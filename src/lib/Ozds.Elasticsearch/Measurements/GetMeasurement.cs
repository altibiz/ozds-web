using System.Threading.Tasks;
using Nest;

namespace Ozds.Elasticsearch
{
  public partial interface IClient
  {
    public IGetResponse<Measurement> GetMeasurement(Id id);

    public Task<IGetResponse<Measurement>> GetMeasurementAsync(Id id);
  };

  public sealed partial class Client : IClient
  {
    public IGetResponse<Measurement> GetMeasurement(
        Id id) => Elasticsearch.Get<Measurement>(id,
        d => d.Index(MeasurementIndexName));

    public async Task<IGetResponse<Measurement>> GetMeasurementAsync(
        Id id) => await Elasticsearch.GetAsync<Measurement>(id,
        d => d.Index(MeasurementIndexName));
  }
}
