using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

[ElasticsearchType(RelationName = "LoadLog", IdProperty = nameof(Id))]
public class LoadLog : IEquatable<LoadLog>, ICloneable
{
  // NOTE: only for elasticsearch purposes
  public const string Type = "load";

  public static string MakeId(string resource) =>
    Strings.CombineIntoStringId("R", resource);

  public LoadLog(
      string resource,
      Period period)
  {
    Resource = resource;
    Period = period;
    Id = MakeId(Resource);
  }

  [Ignore]
  public string Id { get; init; }

  [Keyword(Name = "resource")]
  public string Resource { get; init; }

  [Object(Name = "period")]
  public Period Period { get; init; }

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
      out Period period)
  {
    id = Id;
    resource = Resource;
    period = Period;
  }

  public object Clone() => CloneLoadLog();

  public LoadLog CloneLoadLog() =>
    new LoadLog(
      resource: Resource.CloneString(),
      period:
        new Period
        {
          From = Period.From,
          To = Period.To
        });
};
