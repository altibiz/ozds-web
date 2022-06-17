using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<ISearchResponse<Device>>
  SearchDevicesAsync(
      bool all = false);

  public ISearchResponse<Device>
  SearchDevices(
      bool all = false);

  public Task<ISearchResponse<Device>>
  SearchDevicesBySourceAsync(
      string source,
      bool all = false);

  public ISearchResponse<Device>
  SearchDevicesBySource(
      string source,
      bool all = false);

  public Task<ISearchResponse<Device>>
  SearchDevicesByOwnerAsync(
      string ownerId,
      bool all = false);

  public ISearchResponse<Device>
  SearchDevicesByOwner(
      string ownerId,
      bool all = false);

  public Task<ISearchResponse<Device>>
  SearchDevicesByOwnerUserAsync(
      string ownerUserId,
      bool all = false);

  public ISearchResponse<Device>
  SearchDevicesByOwnerUser(
      string ownerUserId,
      bool all = false);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public ISearchResponse<Device>
  SearchDevices(
      bool all = false) =>
    Elastic.Search<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.All(all)));

  public Task<ISearchResponse<Device>>
  SearchDevicesAsync(
      bool all = false) =>
    Elastic.SearchAsync<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.All(all)));

  public ISearchResponse<Device>
  SearchDevicesBySource(
      string source,
      bool all = false) =>
    Elastic.Search<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.SourceTerm(source) && q.All(all)));

  public Task<ISearchResponse<Device>>
  SearchDevicesBySourceAsync(
      string source,
      bool all = false) =>
    Elastic.SearchAsync<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.SourceTerm(source) && q.All(all)));

  public ISearchResponse<Device>
  SearchDevicesByOwner(
      string ownerId,
      bool all = false) =>
    Elastic.Search<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.OwnerIdTerm(ownerId) && q.All(all)));

  public Task<ISearchResponse<Device>>
  SearchDevicesByOwnerAsync(
      string ownerId,
      bool all = false) =>
    Elastic.SearchAsync<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.OwnerIdTerm(ownerId) && q.All(all)));

  public ISearchResponse<Device>
  SearchDevicesByOwnerUser(
      string ownerUserId,
      bool all = false) =>
    Elastic.Search<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.OwnerUserIdTerm(ownerUserId) && q.All(all)));

  public Task<ISearchResponse<Device>>
  SearchDevicesByOwnerUserAsync(
      string ownerUserId,
      bool all = false) =>
    Elastic.SearchAsync<Device>(s => s
      .Index(DeviceIndexName)
      .Query(q => q.OwnerUserIdTerm(ownerUserId) && q.All(all)));
}
