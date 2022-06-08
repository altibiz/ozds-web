using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<ISearchResponse<LoadLog>>
  SearchLoadLogsAsync(
      string resource,
      int? size = null);

  public ISearchResponse<LoadLog>
  SearchLoadLogs(
      string resource,
      int? size = null);

  public Task<ISearchResponse<LoadLog>>
  SearchLoadLogsSortedByPeriodAsync(
      string resource,
      int? size = null,
      Period? period = null);

  public ISearchResponse<LoadLog>
  SearchLoadLogsSortedByPeriod(
      string resource,
      int? size = null,
      Period? period = null);

  public Task<ISearchResponse<MissingDataLog>>
  SearchMissingDataLogsAsync(
      string resource,
      int? size = null);

  public ISearchResponse<MissingDataLog>
  SearchMissingDataLogs(
      string resource,
      int? size = null);

  public Task<ISearchResponse<MissingDataLog>>
  SearchMissingDataLogsSortedByPeriodAsync(
      string resource,
      int? size = null,
      Period? period = null);

  public ISearchResponse<MissingDataLog>
  SearchMissingDataLogsSortedByPeriod(
      string resource,
      int? size = null,
      Period? period = null);

  public Task<ISearchResponse<MissingDataLog>>
  SearchExtractionMissingDataLogsAsync(
      string resource,
      DateTime due,
      int? size = null,
      Period? period = null);

  public ISearchResponse<MissingDataLog>
  SearchExtractionMissingDataLogs(
      string resource,
      DateTime due,
      int? size = null,
      Period? period = null);

  public Task<ISearchResponse<MissingDataLog>>
  SearchExtractionMissingDataLogsAsync(
      string resource,
      DateTime due,
      int retries,
      int? size = null,
      Period? period = null);

  public ISearchResponse<MissingDataLog>
  SearchExtractionMissingDataLogs(
      string resource,
      DateTime due,
      int retries,
      int? size = null,
      Period? period = null);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<ISearchResponse<LoadLog>>
  SearchLoadLogsAsync(
      string resource,
      int? size = null) =>
    Elasticsearch.SearchAsync<LoadLog>(s => s
      .Query(q => q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, LoadLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName));

  public ISearchResponse<LoadLog>
  SearchLoadLogs(
      string resource,
      int? size = null) =>
    Elasticsearch.Search<LoadLog>(s => s
      .Query(q => q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, LoadLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName));

  public ISearchResponse<LoadLog>
  SearchLoadLogsSortedByPeriod(
      string resource,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.Search<LoadLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, LoadLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Descending(d => d.Period!.To)));

  public Task<ISearchResponse<LoadLog>>
  SearchLoadLogsSortedByPeriodAsync(
      string resource,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.SearchAsync<LoadLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, LoadLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Descending(d => d.Period!.To)));

  public Task<ISearchResponse<MissingDataLog>>
  SearchMissingDataLogsAsync(
      string resource,
      int? size = null) =>
    Elasticsearch.SearchAsync<MissingDataLog>(s => s
      .Query(q => q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName));

  public ISearchResponse<MissingDataLog>
  SearchMissingDataLogs(
      string resource,
      int? size = null) =>
    Elasticsearch.Search<MissingDataLog>(s => s
      .Query(q => q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName));

  public ISearchResponse<MissingDataLog>
  SearchMissingDataLogsSortedByPeriod(
      string resource,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.Search<MissingDataLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Ascending(d => d.Period!.To)));

  public Task<ISearchResponse<MissingDataLog>>
  SearchMissingDataLogsSortedByPeriodAsync(
      string resource,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.SearchAsync<MissingDataLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Ascending(d => d.Period!.To)));

  public ISearchResponse<MissingDataLog>
  SearchExtractionMissingDataLogs(
      string resource,
      DateTime due,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.Search<MissingDataLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .DateRange(r => r
          .Field(f => f.NextExtraction)
          .LessThanOrEquals(due)) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Ascending(d => d.Period!.To)));

  public Task<ISearchResponse<MissingDataLog>>
  SearchExtractionMissingDataLogsAsync(
      string resource,
      DateTime due,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.SearchAsync<MissingDataLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .DateRange(r => r
          .Field(f => f.NextExtraction)
          .LessThanOrEquals(due)) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Ascending(d => d.Period!.To)));

  public ISearchResponse<MissingDataLog>
  SearchExtractionMissingDataLogs(
      string resource,
      DateTime due,
      int retries,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.Search<MissingDataLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .DateRange(r => r
          .Field(f => f.NextExtraction)
          .LessThanOrEquals(due)) && q
        .Range(r => r
          .Field(f => f.Retries)
          .LessThan(retries)) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Ascending(d => d.Period!.To)));

  public Task<ISearchResponse<MissingDataLog>>
  SearchExtractionMissingDataLogsAsync(
      string resource,
      DateTime due,
      int retries,
      int? size = null,
      Period? period = null) =>
    Elasticsearch.SearchAsync<MissingDataLog>(s => s
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Period!.To)
          .GreaterThanOrEquals(
            period?.From ?? DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.MaxValue.ToUniversalTime())) && q
        .DateRange(r => r
          .Field(f => f.NextExtraction)
          .LessThanOrEquals(due)) && q
        .Range(r => r
          .Field(f => f.Retries)
          .LessThan(retries)) && q
        .Term(t => t.Resource, resource) && q
        .Term(t => t.LogType, MissingDataLog.Type))
      .Size(size ?? IElasticsearchClient.MaxSize)
      .Index(LogIndexName)
      .Sort(s => s
        .Ascending(d => d.Period!.To)));
}
