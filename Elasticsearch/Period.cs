using System;
using Nest;

namespace Elasticsearch {
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

  public override string ToString() {
    return $"{From.ToISOString()} - {To.ToISOString()}";
  }
};
}
