using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public GetResponse<Device> GetDevice(Id id);

  public Task<GetResponse<Device>> GetDeviceAsync(Id id);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public GetResponse<Device> GetDevice(Id id) =>
    Elastic.Get<Device>(id, s => s.Index(DeviceIndexName));

  public Task<GetResponse<Device>> GetDeviceAsync(Id id) =>
    Elastic.GetAsync<Device>(id, s => s.Index(DeviceIndexName));
}
