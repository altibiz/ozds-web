using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public ISearchResponse<Log> SearchLoaderLogs(string type, int? size = null);

  public Task<ISearchResponse<Log>> SearchLoaderLogsAsync(
      string type, int? size = null);

  public ISearchResponse<Log> SearchLoaderLogsSorted(
      string type, int? size = null);

  public Task<ISearchResponse<Log>> SearchLoaderLogsSortedAsync(
      string type, int? size = null);

  public ISearchResponse<Log> SearchLoaderLogsSortedByPeriod(
      string type, int? size = null);

  public Task<ISearchResponse<Log>> SearchLoaderLogsSortedByPeriodAsync(
      string type, int? size = null);
};

public sealed partial class Client : IClient {
  public ISearchResponse<Log>
  SearchLoaderLogs(string type, int? size = null) => this._client.Search<Log>(
      s => s.Query(q => q.Term(t => t.Type, type)).Size(size));

  public async Task<ISearchResponse<Log>>
  SearchLoaderLogsAsync(string type, int? size = null) => (
      await this._client.SearchAsync<Log>(
          s => s.Query(q => q.Term(t => t.Type, type)).Size(size)));

  public ISearchResponse<Log> SearchLoaderLogsSorted(
      string type, int? size = null) =>
      this._client.Search<Log>(
          s => s.Query(q => q.Term(t => t.Type, type))
                   .Size(size)
                   .Sort(s => s.Descending(h => h.Timestamp)));

  public async Task<ISearchResponse<Log>>
  SearchLoaderLogsSortedAsync(string type, int? size = null) => (
      await this._client.SearchAsync<Log>(
          s => s.Query(q => q.Term(t => t.Type, type))
                   .Size(size)
                   .Sort(s => s.Descending(d => d.Timestamp))));

  public ISearchResponse<Log> SearchLoaderLogsSortedByPeriod(
      string type, int? size = null) =>
      this._client.Search<Log>(
          s => s.Query(q => q.Term(t => t.Type, type))
                   .Size(size)
  // NOTE: null doesn't matter here because NEST just wants to create a query
#nullable disable
                   .Sort(s => s.Descending(d => d.Data.Period.To)));
#nullable enable

  public async Task<ISearchResponse<Log>>
  SearchLoaderLogsSortedByPeriodAsync(string type, int? size = null) => (
      await this._client.SearchAsync<Log>(
          s => s.Query(q => q.Term(t => t.Type, type))
                   .Size(size)
  // NOTE: null doesn't matter here because NEST just wants to create a query
#nullable disable
                   .Sort(s => s.Descending(d => d.Data.Period.To))));
#nullable enable
}
}
