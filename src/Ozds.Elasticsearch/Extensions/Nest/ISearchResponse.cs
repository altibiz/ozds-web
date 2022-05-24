using Nest;

namespace Ozds.Elasticsearch;

public static class ISearchResponseExtensions
{
  public static IEnumerable<T> Sources<T>(
      this ISearchResponse<T> response) where T : class =>
    response.Hits.Select(hit => hit.Source);

  public static IEnumerable<string> Ids<T>(
      this ISearchResponse<T> response) where T : class =>
    response.Hits.Select(hit => hit.Id);

  public static T? FirstOrDefault<T>(
      this ISearchResponse<T> response) where T : class =>
    response.Hits.FirstOrDefault()?.Source;
}
