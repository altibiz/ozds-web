using System;
using Nest;

namespace Ozds.Elasticsearch
{
  [ElasticsearchType(RelationName = "period")]
  public class Period
  {
    [Date(Name = "from")]
    public DateTime From { get; init; } = DateTime.MinValue.ToUniversalTime();

    [Date(Name = "to")]
    public DateTime To { get; init; } = DateTime.UtcNow;

    public override bool Equals(object? obj) { return Equals(obj as Period); }

    public bool Equals(Period? other)
    {
      return other != null && From == other.From && To == other.To;
    }

    public override int GetHashCode() { return HashCode.Combine(From, To); }

    public override string ToString()
    {
      return $"{From.ToUtcIsoString()} - {To.ToUtcIsoString()}";
    }
  };
}
