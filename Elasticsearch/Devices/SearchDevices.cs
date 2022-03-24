using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public ISearchResponse<Device> SearchDevices(string source, bool all = false);

  public Task<ISearchResponse<Device>> SearchDevicesAsync(
      string source, bool all = false);
};

public sealed partial class Client : IClient {
  public ISearchResponse<Device> SearchDevices(
      string source, bool all = false) =>
      all ? this._client.Search<Device>(
                s => s.Query(q => q.Term(t => t.Source, source)))
          : this._client.Search<Device>(
                s => s.Query(q => q.Term(t => t.Source, source) &&
                                  q.Term(t => t.State, DeviceState.Healthy)));

  public Task<ISearchResponse<Device>> SearchDevicesAsync(
      string source, bool all = false) =>
      all ? this._client.SearchAsync<Device>(
                s => s.Query(q => q.Term(t => t.Source, source)))
          : this._client.SearchAsync<Device>(
                s => s.Query(q => q.Term(t => t.Source, source) &&
                                  q.Term(t => t.State, DeviceState.Healthy)));
}
}
