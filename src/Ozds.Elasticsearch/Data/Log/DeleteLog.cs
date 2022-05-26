using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<DeleteResponse> DeleteLoadLogAsync(Id id);

  public DeleteResponse DeleteLoadLog(Id id);
};

public sealed partial class Client : IClient
{
  public Task<DeleteResponse> DeleteLoadLogAsync(Id id) =>
    Elasticsearch.DeleteAsync(
      DocumentPath<LoadLog>.Id(id),
      s => s
        .Index(LogIndexName));

  public DeleteResponse DeleteLoadLog(Id id) =>
    Elasticsearch.Delete(
      DocumentPath<LoadLog>.Id(id),
      s => s
        .Index(LogIndexName));

  public Task<DeleteResponse> DeleteMissingDataLogAsync(Id id) =>
    Elasticsearch.DeleteAsync(
      DocumentPath<MissingDataLog>.Id(id),
      s => s
        .Index(LogIndexName));

  public DeleteResponse DeleteMissingDataLog(Id id) =>
    Elasticsearch.Delete(
      DocumentPath<MissingDataLog>.Id(id),
      s => s
        .Index(LogIndexName));
}
