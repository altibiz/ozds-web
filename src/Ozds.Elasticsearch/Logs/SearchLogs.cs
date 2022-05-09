using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<ISearchResponse<Log>> SearchLogsAsync(
      string type, int? size = null);

  public ISearchResponse<Log> SearchLogs(
      string type, int? size = null);

  public Task<ISearchResponse<Log>> SearchLogsSortedAsync(
      string type, int? size = null);

  public ISearchResponse<Log> SearchLogsSorted(
      string type, int? size = null);

  public Task<ISearchResponse<Log>> SearchLogsSortedByPeriodAsync(
      string type, int? size = null);

  public ISearchResponse<Log> SearchLogsSortedByPeriod(
      string type, int? size = null);

  public Task<ISearchResponse<Log>> SearchLoadLogsSortedByPeriodAsync(
      string source, int? size = null);

  public ISearchResponse<Log> SearchLoadLogsSortedByPeriod(
      string source, int? size = null);
};

public sealed partial class Client : IClient
{
  public Task<ISearchResponse<Log>> SearchLogsAsync(
      string type, int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName));

  public ISearchResponse<Log> SearchLogs(
      string type, int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName));

  public Task<ISearchResponse<Log>>
  SearchLogsSortedAsync(
      string type, int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(LogIndexName));

  public ISearchResponse<Log> SearchLogsSorted(
      string type, int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(LogIndexName));

  public ISearchResponse<Log> SearchLogsSortedByPeriod(
      string type, int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        // NOTE: null doesn't matter because it is an expression
        .Descending(d => d.Data.Period!.To)));

  public Task<ISearchResponse<Log>> SearchLogsSortedByPeriodAsync(
      string type, int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        // NOTE: null doesn't matter because it is an expression
        .Descending(d => d.Data.Period!.To)));

  public ISearchResponse<Log> SearchLoadLogsSortedByPeriod(
      string source, int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, LogType.LoadEnd) && q
        .Term(t => t.Source, source))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        // NOTE: null doesn't matter because it is an expression
        .Descending(d => d.Data.Period!.To)));

  public Task<ISearchResponse<Log>> SearchLoadLogsSortedByPeriodAsync(
      string source, int? size = null) => (
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, LogType.LoadEnd) && q
        .Term(t => t.Source, source))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        // NOTE: null doesn't matter because it is an expression
        .Descending(d => d.Data.Period!.To))));
}
