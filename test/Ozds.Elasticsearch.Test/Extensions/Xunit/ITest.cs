using Xunit.Abstractions;
using Ozds.Extensions;

namespace Ozds.Elasticsearch.Test;

public static class ITestExtensions
{
  // NOTE: change as needed
  public static string? GetCorrespondingIndexName(this ITest test) =>
    "test." +
    test?.DisplayName
      .RegexReplace(@"^.+\.(.+?)(?:Test)?\(.*$", @"$1")
      .RegexReplace(@"([a-z])([A-Z])", @"$1-$2")
      .ToLowerInvariant();
}
