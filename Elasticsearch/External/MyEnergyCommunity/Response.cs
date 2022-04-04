using System.Collections.Generic;

namespace Elasticsearch.MyEnergyCommunity
{
  public class Response<T>
  {
    public string continuationToken { get; init; } = default!;
    public IEnumerable<T> items { get; init; } = default!;
  }
}
