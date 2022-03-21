using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient {
  public IEnumerable<LoaderLog> getLoaderLogs(string type, int? size = null);

  public Task<IEnumerable<LoaderLog>> getLoaderLogsAsync(
      string type, int? size = null);

  public IEnumerable<LoaderLog> getLoaderLogsSorted(
      string type, int? size = null);

  public Task<IEnumerable<LoaderLog>> getLoaderLogsSortedAsync(
      string type, int? size = null);
};

public sealed partial class Client : IClient {
  public IEnumerable<LoaderLog> getLoaderLogs(string type, int? size = null) =>
      this._client
          .Search<LoaderLog>(
              search => search.Index(Client.LoaderLogIndexName)
                            .Query(q => q.Term(
                                       t => t.Field(f => f.type).Value(type)))
                            .Size(size))
          .Hits.Select(hit => hit.Source);

  public async Task<IEnumerable<LoaderLog>> getLoaderLogsAsync(
      string type, int? size = null) =>
      (await this._client.SearchAsync<LoaderLog>(
           search => search.Index(Client.LoaderLogIndexName)
                         .Query(
                             q => q.Term(t => t.Field(f => f.type).Value(type)))
                         .Size(size)))
          .Hits.Select(hit => hit.Source);

  public IEnumerable<LoaderLog> getLoaderLogsSorted(
      string type, int? size = null) =>
      this._client
          .Search<LoaderLog>(
              search => search.Index(Client.LoaderLogIndexName)
                            .Query(q => q.Term(
                                       t => t.Field(f => f.type).Value(type)))
                            .Size(size)
                            .Sort(s => s.Descending(h => h.timestamp)))
          .Hits.Select(hit => hit.Source);

  public async Task<IEnumerable<LoaderLog>> getLoaderLogsSortedAsync(
      string type, int? size = null) =>
      (await this._client.SearchAsync<LoaderLog>(
           search => search.Index(Client.LoaderLogIndexName)
                         .Query(
                             q => q.Term(t => t.Field(f => f.type).Value(type)))
                         .Size(size)
                         .Sort(s => s.Descending(d => d.timestamp))))
          .Hits.Select(hit => hit.Source);
}
}
