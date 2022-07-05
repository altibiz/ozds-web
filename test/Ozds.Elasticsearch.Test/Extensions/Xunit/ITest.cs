using Xunit.Abstractions;
using Ozds.Extensions;

namespace Ozds.Elasticsearch.Test;

public static class ITestExtensions
{
  public static string? GetIndexSuffix(this ITest? test) =>
    test?.DisplayName
      .RegexReplace(@"^.+\.(.+?)(?:Test)?\(.*$", @"$1")
      .RegexReplace(@"([a-z])([A-Z])", @"$1-$2")
      .ToLowerInvariant();
}
