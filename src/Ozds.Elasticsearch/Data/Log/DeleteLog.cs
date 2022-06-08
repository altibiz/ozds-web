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
