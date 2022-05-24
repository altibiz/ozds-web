using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

public class LogType
{
  public const string ExtractionBegin = "extractionBegin";
  public const string ExtractionEnd = "extractionEnd";
  public const string LoadBegin = "loadBegin";
  public const string LoadEnd = "loadEnd";
  public const string MissingData = "missingData";
  public const string DuplicatedData = "duplicatedData";
  public const string InvalidData = "invalidData";
};

[ElasticsearchType(RelationName = "Log", IdProperty = nameof(Id))]
public class Log
{
  public static string MakeId(DateTime timestamp) =>
    Strings.CombineIntoStringId(timestamp.ToUtcIsoString());

  public Log(
      string type,
      string? resource = null,
      KnownData? data = null)
  {
    Timestamp = DateTime.UtcNow;
    Type = type;
    Resource = resource;
    Data = data ?? new KnownData { };
    Id = MakeId(Timestamp);
  }

  public string Id { get; init; }

  [Date(Name = "timestamp")]
  public DateTime Timestamp { get; init; }

  [Keyword(Name = "type")]
  public string Type { get; init; }

  [Keyword(Name = "resource")]
  public string? Resource { get; init; }

  [Object(Name = "data")]
  public KnownData Data { get; init; }

  [ElasticsearchType(RelationName = "LogData")]
  public class KnownData
  {
    [Object(Name = "period")]
    public Period? Period { get; init; } = null;

    [Date(Name = "lastExtraction")]
    public DateTime? LastExtraction { get; init; } = null;

    [Date(Name = "nextExtraction")]
    public DateTime? NextExtraction { get; init; } = null;

    [Number(NumberType.Integer, Name = "retries")]
    public int? Retries { get; init; } = null;

    [Boolean(Name = "shouldValidate")]
    public bool? ShouldValidate { get; init; } = null;

    [Text(Name = "error")]
    public string? Error { get; init; } = null;
  }

  public override bool Equals(object? obj) =>
    Equals(obj as Device);

  public bool Equals(Device? other) =>
    other is not null &&
    Id == other.Id;

  public override int GetHashCode() =>
    Id.GetHashCode();
};
