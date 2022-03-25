using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public BulkResponse IndexLoaderLogs(IEnumerable<Loader.Log> devices);
  public Task<BulkResponse> IndexLoaderLogsAsync(
      IEnumerable<Loader.Log> devices);
};

public sealed partial class Client : IClient {
  public BulkResponse IndexLoaderLogs(IEnumerable<Loader.Log> devices) =>
      this._client.Bulk(s => s.IndexMany(devices));

  public Task<BulkResponse> IndexLoaderLogsAsync(
      IEnumerable<Loader.Log> devices) =>
      this._client.BulkAsync(s => s.IndexMany(devices));
}
}
