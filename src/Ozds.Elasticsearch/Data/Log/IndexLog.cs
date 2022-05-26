using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<IndexResponse> IndexLoadLogAsync(LoadLog log);

  public IndexResponse IndexLoadLog(LoadLog log);

  public Task<IndexResponse> IndexMissingDataLogAsync(MissingDataLog log);

  public IndexResponse IndexMissingDataLog(MissingDataLog log);
};

public sealed partial class Client : IClient
{
  public Task<IndexResponse> IndexLoadLogAsync(LoadLog log) =>
    Elasticsearch
      .IndexAsync(log, s => s
        .Index(LogIndexName));

  public IndexResponse IndexLoadLog(LoadLog log) =>
    Elasticsearch
      .Index(log, s => s
          .Index(LogIndexName));

  public Task<IndexResponse> IndexMissingDataLogAsync(MissingDataLog log) =>
    Elasticsearch
      .IndexAsync(log, s => s
        .Index(LogIndexName));

  public IndexResponse IndexMissingDataLog(MissingDataLog log) =>
    Elasticsearch
      .Index(log, s => s
          .Index(LogIndexName));
}
