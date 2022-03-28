using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch {
public partial interface IClient {
  public BulkResponse DeleteLoaderLogs(IEnumerable<Id> loaderLogIds);
  public Task<BulkResponse> DeleteLoaderLogsAsync(IEnumerable<Id> loaderLogIds);
};

public sealed partial class Client : IClient {
  public BulkResponse
  DeleteLoaderLogs(IEnumerable<Id> loaderLogIds) => this._client.Bulk(
      s => s.DeleteMany<Log>(loaderLogIds.ToStrings()));

  public Task<BulkResponse>
  DeleteLoaderLogsAsync(IEnumerable<Id> loaderLogIds) => this._client.BulkAsync(
      s => s.DeleteMany<Log>(loaderLogIds.ToStrings()));
}
}
