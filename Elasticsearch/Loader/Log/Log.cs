using System;
using Nest;

namespace Elasticsearch {
public static partial class Loader {
  public class LogType {
    public const string LoadBegin = "loadBegin";
    public const string LoadEnd = "loadEnd";
    public const string MissingData = "missingData";
    public const string DuplicatedData = "duplicatedData";
    public const string InvalidData = "invalidData";
  };

  [ElasticsearchType(RelationName = "loaderLog")]
  public class Log {

    [Date(Name = "timestamp")]
    public DateTime Timestamp { get; init; } = default!;

    [Keyword(Name = "type")]
    public string Type { get; init; } = default!;

    [Object(Name = "period")]
    public Period Period { get; init; } = default!;

    [Keyword(Name = "source")]
    public string? Source { get; init; } = default;

    [Text(Name = "description")]
    public string? Description { get; init; } = default;

    public override bool Equals(object? obj) { return Equals(obj as Log); }

    public bool Equals(Log? other) {
      return other != null && Type == other.Type &&
             Timestamp == other.Timestamp && Source == other.Source;
    }

    public override int GetHashCode() {
      return HashCode.Combine(Type, Timestamp, Source);
    }
  };

  [ElasticsearchType(RelationName = "period")]
  public class Period {
    [Date(Name = "from")]
    public DateTime From { get; init; } = default!;

    [Date(Name = "to")]
    public DateTime To { get; init; } = default!;

    public override bool Equals(object? obj) { return Equals(obj as Period); }

    public bool Equals(Period? other) {
      return other != null && From == other.From && To == other.To;
    }

    public override int GetHashCode() { return HashCode.Combine(From, To); }
  }
}
}
