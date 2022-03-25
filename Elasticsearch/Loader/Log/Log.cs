using System;
using Nest;

namespace Elasticsearch {
public static partial class Loader {
  public const string Source = "loader";

  public class LogType {
    public const string LoadBegin = "loadBegin";
    public const string LoadEnd = "loadEnd";
    public const string MissingData = "missingData";
    public const string DuplicatedData = "duplicatedData";
    public const string InvalidData = "invalidData";
  };

  [ElasticsearchType(RelationName = "loaderLog", IdProperty = nameof(Id))]
  public class Log {
    public Log(string type, string? source = null, KnownData? data = null) {
      Timestamp = DateTime.Now;
      Type = type;
      Source = source ?? Loader.Source;
      Data = data ?? new KnownData {};
    }

    public Log(string type, KnownData? data = null) : this(type, null, data) {}

    public string Id { get; init; } = Guid.NewGuid().ToString();

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

  [ElasticsearchType(RelationName = "period")]
  public class Period {
    [Date(Name = "from")]
    public DateTime From { get; init; } = DateTime.MinValue;

    [Date(Name = "to")]
    public DateTime To { get; init; } = DateTime.Now;

    public override bool Equals(object? obj) { return Equals(obj as Period); }

    public bool Equals(Period? other) {
      return other != null && From == other.From && To == other.To;
    }

    public override int GetHashCode() { return HashCode.Combine(From, To); }
  }
}
}
