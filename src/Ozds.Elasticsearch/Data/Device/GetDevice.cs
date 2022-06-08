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
    Elasticsearch.Get<Device>(id, s => s.Index(DeviceIndexName));

  public Task<GetResponse<Device>> GetDeviceAsync(Id id) =>
    Elasticsearch.GetAsync<Device>(id, s => s.Index(DeviceIndexName));
}
