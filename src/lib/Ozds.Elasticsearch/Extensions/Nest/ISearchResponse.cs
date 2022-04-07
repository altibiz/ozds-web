using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Ozds.Elasticsearch
{
  public static class ISearchResponseExtensions
  {
    public static IEnumerable<T> Sources<T>(this ISearchResponse<T> response)
        where T : class
    { return response.Hits.Select(hit => hit.Source); }

    public static IEnumerable<string> Ids<T>(this ISearchResponse<T> response)
        where T : class
    { return response.Hits.Select(hit => hit.Id); }
  }
}
