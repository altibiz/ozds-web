using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public ISearchResponse<Loader.Log> SearchLoaderLogs(
      string type, int? size = null);

  public Task<ISearchResponse<Loader.Log>> SearchLoaderLogsAsync(
      string type, int? size = null);

  public ISearchResponse<Loader.Log> SearchLoaderLogsSorted(
      string type, int? size = null);

  public Task<ISearchResponse<Loader.Log>> SearchLoaderLogsSortedAsync(
      string type, int? size = null);

  public ISearchResponse<Loader.Log> SearchLoaderLogsSortedByPeriod(
      string type, int? size = null);

  public Task<ISearchResponse<Loader.Log>> SearchLoaderLogsSortedByPeriodAsync(
      string type, int? size = null);
};

public sealed partial class Client : IClient {
  public ISearchResponse<Loader.Log> SearchLoaderLogs(
      string type, int? size = null) =>
      this._client.Search<Loader.Log>(
          s => s
                   .Query(q => q.Term(t => t.Type, type))
                   .Size(size));

  public async Task<ISearchResponse<Loader.Log>>
  SearchLoaderLogsAsync(string type, int? size = null) => (
      await this._client.SearchAsync<Loader.Log>(
          s => s
                   .Query(q => q.Term(t => t.Type, type))
                   .Size(size)));

  public ISearchResponse<Loader.Log> SearchLoaderLogsSorted(
      string type, int? size = null) =>
      this._client.Search<Loader.Log>(
          s => s
                   .Query(q => q.Term(t => t.Type, type))
                   .Size(size)
                   .Sort(s => s.Descending(h => h.Timestamp)));

  public async Task<ISearchResponse<Loader.Log>>
  SearchLoaderLogsSortedAsync(string type, int? size = null) => (
      await this._client.SearchAsync<Loader.Log>(
          s => s
                   .Query(q => q.Term(t => t.Type, type))
                   .Size(size)
                   .Sort(s => s.Descending(d => d.Timestamp))));

  public ISearchResponse<Loader.Log> SearchLoaderLogsSortedByPeriod(
      string type, int? size = null) =>
      this._client.Search<Loader.Log>(
          s => s
                   .Query(q => q.Term(t => t.Type, type))
                   .Size(size)
                   .Sort(s => s.Descending(h => h.Period.To)));

  public async Task<ISearchResponse<Loader.Log>>
  SearchLoaderLogsSortedByPeriodAsync(string type, int? size = null) => (
      await this._client.SearchAsync<Loader.Log>(
          s => s
                   .Query(q => q.Term(t => t.Type, type))
                   .Size(size)
                   .Sort(s => s.Descending(d => d.Period.To))));
}
}
