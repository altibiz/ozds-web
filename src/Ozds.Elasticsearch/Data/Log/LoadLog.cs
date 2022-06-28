using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

[ElasticsearchType(RelationName = "loadLog", IdProperty = nameof(Id))]
public class LoadLog : IEquatable<LoadLog>, ICloneable
{
  // NOTE: only for elasticsearch purposes
  public const string Type = "load";

  public static string MakeId(
      string resource) =>
    Strings.CombineIntoStringId(
      "R", resource);

  public LoadLog(
      string resource,
      Period period,
      DateTime? lastValidation = null)
  {
    var now = DateTime.UtcNow;

    Resource = resource;
    Period = period;
    LastValidation = lastValidation ?? now;
  }

  [Ignore]
  public string Id
  {
    get =>
      MakeId(
        Resource);
  }

  [Keyword(Name = "resource")]
  public string Resource { get; init; }

  [Object(Name = "period")]
  public Period Period { get; init; }

  [Date(Name = "lastValidation")]
  public DateTime LastValidation { get; init; }

  // NOTE: only for elasticsearch purposes
  [Keyword(Name = "type")]
  public string LogType { get; init; } = Type;

  public override bool Equals(object? obj) =>
    Equals(obj as LoadLog);

  public bool Equals(LoadLog? other) =>
    other is not null &&
    Id == other.Id;

  public override int GetHashCode() =>
    Id.GetHashCode();

  public void Deconstruct(
      out string id,
      out string resource,
      out Period period,
      out DateTime lastValidation)
  {
    id = Id;
    resource = Resource;
    period = Period;
    lastValidation = LastValidation;
  }

  public object Clone() => CloneLoadLog();

  public LoadLog CloneLoadLog() =>
    new LoadLog(
      resource: Resource.CloneString(),
      period: Period.ClonePeriod(),
      lastValidation: LastValidation);
};
