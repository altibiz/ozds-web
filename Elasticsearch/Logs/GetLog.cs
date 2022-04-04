using System.Threading.Tasks;
using Nest;

namespace Elasticsearch
{
  public partial interface IClient
  {
    public IGetResponse<Log> GetLog(Id id);

    public Task<IGetResponse<Log>> GetLogAsync(Id id);
  };

  public sealed partial class Client : IClient
  {
    public IGetResponse<Log> GetLog(Id id) => Elasticsearch.Get<Log>(
        id, d => d.Index(LogIndexName));

    public async Task<IGetResponse<Log>> GetLogAsync(
        Id id) => await Elasticsearch.GetAsync<Log>(id,
        d => d.Index(LogIndexName));
  }
}
