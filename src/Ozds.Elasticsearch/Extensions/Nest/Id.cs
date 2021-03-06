using Nest;

namespace Ozds.Elasticsearch;

public static class IdExtensions
{
  public static IEnumerable<string> ToStrings(
      this IEnumerable<Id> ids) => ids.Select(i => i.ToString());

  public static IEnumerable<Id> ToIds(
      this IEnumerable<string> strings) => strings.Select(s => new Id(s));
}
