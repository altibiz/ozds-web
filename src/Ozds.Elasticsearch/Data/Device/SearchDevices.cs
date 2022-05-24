using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public ISearchResponse<Device>
  SearchDevicesBySource(
      string source,
      bool all = false);

  public Task<ISearchResponse<Device>>
  SearchDevicesBySourceAsync(
      string source,
      bool all = false);
};

public sealed partial class Client : IClient
{
  public ISearchResponse<Device>
  SearchDevicesBySource(
      string source,
      bool all = false) =>
    all
    ? this.Elasticsearch.Search<Device>(s => s
        .Query(q => q
          .Term(t => t.Source, source))
        .Index(DeviceIndexName))
    : this.Elasticsearch.Search<Device>(s => s
        .Query(q => q
          .Term(t => t.Source, source) && q
          .Term(t => t.State, DeviceState.Active))
        .Index(DeviceIndexName));

  public Task<ISearchResponse<Device>>
  SearchDevicesBySourceAsync(
      string source,
      bool all = false) =>
    all
    ? this.Elasticsearch.SearchAsync<Device>(s => s
        .Query(q => q
          .Term(t => t.Source, source))
        .Index(DeviceIndexName))
    : this.Elasticsearch.SearchAsync<Device>(s => s
        .Query(q => q
          .Term(t => t.Source, source) && q
          .Term(t => t.State, DeviceState.Active))
        .Index(DeviceIndexName));
}
