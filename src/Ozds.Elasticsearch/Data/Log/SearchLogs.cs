using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<ISearchResponse<Log>>
  SearchLogsAsync(
      string type,
      int? size = null);

  public ISearchResponse<Log>
  SearchLogs(
      string type,
      int? size = null);

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByTimestampAsync(
      string type,
      int? size = null);

  public ISearchResponse<Log>
  SearchLogsSortedByTimestamp(
      string type,
      int? size = null);

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByPeriodAsync(
      string type,
      int? size = null);

  public ISearchResponse<Log>
  SearchLogsSortedByPeriod(
      string type,
      int? size = null);

  public Task<ISearchResponse<Log>>
  SearchLogsAsync(
      string type,
      string resource,
      int? size = null);

  public ISearchResponse<Log>
  SearchLogs(
      string type,
      string resource,
      int? size = null);

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByTimestampAsync(
      string type,
      string resource,
      int? size = null);

  public ISearchResponse<Log>
  SearchLogsSortedByTimestamp(
      string type,
      string resource,
      int? size = null);

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByPeriodAsync(
      string type,
      string resource,
      int? size = null);

  public ISearchResponse<Log>
  SearchLogsSortedByPeriod(
      string type,
      string resource,
      int? size = null);
};

public sealed partial class Client : IClient
{
  public Task<ISearchResponse<Log>>
  SearchLogsAsync(
      string type,
      int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName));

  public ISearchResponse<Log>
  SearchLogs(
      string type,
      int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName));

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByTimestampAsync(
      string type,
      int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(LogIndexName));

  public ISearchResponse<Log>
  SearchLogsSortedByTimestamp(
      string type,
      int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(LogIndexName));

  public ISearchResponse<Log>
  SearchLogsSortedByPeriod(
      string type,
      int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        .Descending(d => d.Data.Period!.To)));

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByPeriodAsync(
      string type,
      int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        .Descending(d => d.Data.Period!.To)));

  public Task<ISearchResponse<Log>>
  SearchLogsAsync(
      string type,
      string resource,
      int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type) && q
        .Term(t => t.Resource, resource))
      .Size(size)
      .Index(LogIndexName));

  public ISearchResponse<Log>
  SearchLogs(
      string type,
      string resource,
      int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type) && q
        .Term(t => t.Resource, resource))
      .Size(size)
      .Index(LogIndexName));

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByTimestampAsync(
      string type,
      string resource,
      int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type) && q
        .Term(t => t.Resource, resource))
      .Size(size)
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(LogIndexName));

  public ISearchResponse<Log>
  SearchLogsSortedByTimestamp(
      string type,
      string resource,
      int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type) && q
        .Term(t => t.Resource, resource))
      .Size(size)
      .Sort(s => s
        .Descending(h => h.Timestamp))
      .Index(LogIndexName));

  public ISearchResponse<Log>
  SearchLogsSortedByPeriod(
      string type,
      string resource,
      int? size = null) =>
    Elasticsearch.Search<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type) && q
        .Term(t => t.Resource, resource))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        .Descending(d => d.Data.Period!.To)));

  public Task<ISearchResponse<Log>>
  SearchLogsSortedByPeriodAsync(
      string type,
      string resource,
      int? size = null) =>
    Elasticsearch.SearchAsync<Log>(s => s
      .Query(q => q
        .Term(t => t.Type, type) && q
        .Term(t => t.Resource, resource))
      .Size(size)
      .Index(LogIndexName)
      .Sort(s => s
        .Descending(d => d.Data.Period!.To)));
}
