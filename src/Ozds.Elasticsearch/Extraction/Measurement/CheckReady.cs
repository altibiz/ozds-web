using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementExtractor { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  // TODO: check by index names when it is more reliable
  // NOTE: now its like this "description": "reindex from [<source>] to [<dest>]"
  public Task<bool> CheckReadyAsync() =>
    Elastic.Tasks
      .ListAsync(l => l
        .Actions("*reindex")
        .Detailed())
      .Then(response => !response.Nodes.Any());

  // TODO: check by index names when it is more reliable
  // NOTE: now its like this "description": "reindex from [<source>] to [<dest>]"
  public bool CheckReady() =>
    Elastic.Tasks
      .List(l => l
        .Actions("*reindex")
        .Detailed())
      .Var(response => !response.Nodes.Any());
}
