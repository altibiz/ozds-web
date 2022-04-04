using System.Threading.Tasks;
using Nest;

namespace Elasticsearch
{
  public partial interface IClient
  {
    public ISearchResponse<Log> SearchLogs(string type, int? size = null);

    public Task<ISearchResponse<Log>> SearchLogsAsync(
        string type, int? size = null);

    public ISearchResponse<Log> SearchLogsSorted(string type, int? size = null);

    public Task<ISearchResponse<Log>> SearchLogsSortedAsync(
        string type, int? size = null);

    public ISearchResponse<Log> SearchLogsSortedByPeriod(
        string type, int? size = null);

    public Task<ISearchResponse<Log>> SearchLogsSortedByPeriodAsync(
        string type, int? size = null);

    public ISearchResponse<Log> SearchLoadLogsSortedByPeriod(
        string source, int? size = null);

    public Task<ISearchResponse<Log>> SearchLoadLogsSortedByPeriodAsync(
        string source, int? size = null);
  };

  public sealed partial class Client : IClient
  {
    public ISearchResponse<Log>
    SearchLogs(string type, int? size = null) => this.Elasticsearch.Search<Log>(
        s => s.Query(q => q.Term(t => t.Type, type))
                 .Size(size)
                 .Index(LogIndexName));

    public async Task<ISearchResponse<Log>>
    SearchLogsAsync(string type, int? size = null) => (
        await this.Elasticsearch.SearchAsync<Log>(
            s => s.Query(q => q.Term(t => t.Type, type))
                     .Size(size)
                     .Index(LogIndexName)));

    public ISearchResponse<Log> SearchLogsSorted(string type, int? size = null) =>
        this.Elasticsearch.Search<Log>(
            s => s.Query(q => q.Term(t => t.Type, type))
                     .Size(size)
                     .Sort(s => s.Descending(h => h.Timestamp))
                     .Index(LogIndexName));

    public async Task<ISearchResponse<Log>>
    SearchLogsSortedAsync(string type, int? size = null) => (
        await this.Elasticsearch.SearchAsync<Log>(
            s => s.Query(q => q.Term(t => t.Type, type))
                     .Size(size)
                     .Sort(s => s.Descending(d => d.Timestamp))
                     .Index(LogIndexName)));

    public ISearchResponse<Log> SearchLogsSortedByPeriod(
        string type, int? size = null) =>
        this.Elasticsearch.Search<Log>(
            s => s.Query(q => q.Term(t => t.Type, type))
                     .Size(size)
                     .Index(LogIndexName)
                   // NOTE: null doesn't matter here because NEST just wants to create a query
#nullable disable
                   .Sort(s => s.Descending(d => d.Data.Period.To)));
#nullable enable

    public async Task<ISearchResponse<Log>>
    SearchLogsSortedByPeriodAsync(string type, int? size = null) => (
        await this.Elasticsearch.SearchAsync<Log>(
            s => s.Query(q => q.Term(t => t.Type, type))
                     .Size(size)
                     .Index(LogIndexName)
                   // NOTE: null doesn't matter here because NEST just wants to create a query
#nullable disable
                   .Sort(s => s.Descending(d => d.Data.Period.To))));
#nullable enable

    public ISearchResponse<Log> SearchLoadLogsSortedByPeriod(
        string source, int? size = null) =>
        this.Elasticsearch.Search<Log>(
            s => s.Query(q => q.Term(t => t.Type, LogType.LoadEnd) &&
                              q.Term(t => t.Source, source))
                     .Size(size)
                     .Index(LogIndexName)
                   // NOTE: null doesn't matter here because NEST just wants to create a query
#nullable disable
                   .Sort(s => s.Descending(d => d.Data.Period.To)));
#nullable enable

    public async Task<ISearchResponse<Log>>
    SearchLoadLogsSortedByPeriodAsync(string source, int? size = null) => (
        await this.Elasticsearch.SearchAsync<Log>(
            s => s.Query(q => q.Term(t => t.Type, LogType.LoadEnd) &&
                              q.Term(t => t.Source, source))
                     .Size(size)
                     .Index(LogIndexName)
                   // NOTE: null doesn't matter here because NEST just wants to create a query
#nullable disable
                   .Sort(s => s.Descending(d => d.Data.Period.To))));
#nullable enable
  }
}
