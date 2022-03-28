using System;
using Nest;

namespace Elasticsearch {
public class LogType {
  public const string LoadBegin = "loadBegin";
  public const string LoadEnd = "loadEnd";
  public const string MissingData = "missingData";
  public const string DuplicatedData = "duplicatedData";
  public const string InvalidData = "invalidData";
};

[ElasticsearchType(RelationName = "loaderLog", IdProperty = nameof(Id))]
public class Log {
  // TODO: review if timestamp is enough
  public static string MakeId(DateTime timestamp) {
    return StringExtensions.CombineIntoStringId(timestamp.ToISOString());
  }

  public Log(string type, string? source = null, KnownData? data = null) {
    Timestamp = DateTime.Now;
    Type = type;
    Source = source ?? "";
    Data = data ?? new KnownData {};
    Id = MakeId(Timestamp);
  }

  public Log(string type, KnownData? data = null) : this(type, null, data) {}

  public string Id { get; init; }

  [Date(Name = "timestamp")]
  public DateTime Timestamp { get; init; }

  [Keyword(Name = "type")]
  public string Type { get; init; }

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Object(Name = "data")]
  public KnownData Data { get; init; }

  public override bool Equals(object? obj) { return Equals(obj as Log); }

  public bool Equals(Log? other) {
    return other != null && Type == other.Type &&
           Timestamp == other.Timestamp && Source == other.Source;
  }

  public override int GetHashCode() {
    return HashCode.Combine(Type, Timestamp, Source);
  }

  [ElasticsearchType(RelationName = "loaderLogData")]
  public class KnownData {
    [Object(Name = "period")]
    public Period? Period { get; init; } = null;
  }
};
}
