using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<DeleteResponse> DeleteLoadLogAsync(Id id);

  public DeleteResponse DeleteLoadLog(Id id);

  public Task<DeleteResponse> DeleteMissingDataLogAsync(Id id);

  public DeleteResponse DeleteMissingDataLog(Id id);
};

// TODO: without the type things
public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<DeleteResponse> DeleteLoadLogAsync(Id id) =>
    Elastic.DeleteAsync(
      DocumentPath<LoadLog>.Id(id),
      s => s
        .Index(LogIndexName));

  public DeleteResponse DeleteLoadLog(Id id) =>
    Elastic.Delete(
      DocumentPath<LoadLog>.Id(id),
      s => s
        .Index(LogIndexName));

  public Task<DeleteResponse> DeleteMissingDataLogAsync(Id id) =>
    Elastic.DeleteAsync(
      DocumentPath<MissingDataLog>.Id(id),
      s => s
        .Index(LogIndexName));

  public DeleteResponse DeleteMissingDataLog(Id id) =>
    Elastic.Delete(
      DocumentPath<MissingDataLog>.Id(id),
      s => s
        .Index(LogIndexName));
}
