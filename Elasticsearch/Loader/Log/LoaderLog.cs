using System;

// NOTE: this has to be a nullable type for NEST
// TODO: attribute mapping?

namespace Elasticsearch {
public static partial class Loader {
  public class LogType {
    public const string LoadBegin = "loadBegin";
    public const string LoadEnd = "loadEnd";
    public const string MissingData = "missingData";
    public const string DuplicatedData = "duplicatedData";
    public const string InvalidData = "invalidData";
  };

  public class Log {
    public DateTime timestamp { get; init; } = default!;
    public string type { get; init; } = default!;
    public Period? period { get; init; } = default!;
    public string? description { get; init; } = default!;
  };

  public class Period {
    public DateTime from { get; init; } = default!;
    public DateTime to { get; init; } = default!;
  }
}
}
