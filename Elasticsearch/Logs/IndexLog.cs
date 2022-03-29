using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public IndexResponse IndexLog(Log log);
  public Task<IndexResponse> IndexLogAsync(Log log);
};

public sealed partial class Client : IClient {
  public IndexResponse IndexLog(Log log) => this.Elasticsearch.Index(
      log, s => s.Index(LogIndexName));

  public async Task<IndexResponse> IndexLogAsync(
      Log log) => await this.Elasticsearch.IndexAsync(log,
      s => s.Index(LogIndexName));
}
}
