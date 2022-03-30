using Xunit.Abstractions;

namespace Elasticsearch.Test;

public static class ITestExtensions {
  public static string? GetCorrespondingIndexName(
      this ITest test) => test?.DisplayName.RegexReplace(@"^.+\.(.+?)$", @"$1")
                              .RegexReplace(@"([a-z])([A-Z])", @"$1-$2")
                              .ToLowerInvariant();
}
