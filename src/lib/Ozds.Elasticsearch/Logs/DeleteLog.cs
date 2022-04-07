using System.Threading.Tasks;
using Nest;

namespace Ozds.Elasticsearch
{
  public partial interface IClient
  {
    public DeleteResponse DeleteLog(Id id);
    public Task<DeleteResponse> DeleteLogAsync(Id id);
  };

  public sealed partial class Client : IClient
  {
    public DeleteResponse DeleteLog(Id id) => this.Elasticsearch.Delete(
        DocumentPath<Log>.Id(id), s => s.Index(LogIndexName));

    public async Task<DeleteResponse> DeleteLogAsync(
        Id id) => await this.Elasticsearch.DeleteAsync(DocumentPath<Log>.Id(id),
        s => s.Index(LogIndexName));
  }
}
