using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds);

  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<BulkResponse> DeleteDevicesAsync(IEnumerable<Id> deviceIds) =>
    Elastic.BulkAsync(s => s
      .DeleteMany<Device>(deviceIds.ToStrings())
      .RefreshInDevelopment(Env)
      .Index(DeviceIndexName));

  public BulkResponse DeleteDevices(IEnumerable<Id> deviceIds) =>
    Elastic.Bulk(s => s
      .DeleteMany<Device>(deviceIds.ToStrings())
      .RefreshInDevelopment(Env)
      .Index(DeviceIndexName));
}
