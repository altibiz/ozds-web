using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public static class MissingDataLogErrorCode
{
  public const string Provider = "provider";
  public const string Validation = "validation";
  public const string Consistency = "consistency";
}

[ElasticsearchType(RelationName = "missingDataLog", IdProperty = nameof(Id))]
public class MissingDataLog : IEquatable<MissingDataLog>, ICloneable
{
  // NOTE: only for elasticsearch purposes
  public const string Type = "missingData";

  public static string MakeId(
      string resource,
      Period period) =>
    Strings.CombineIntoStringId(
      "R", resource,
      "F", period.From.ToUtcIsoString(),
      "T", period.To.ToUtcIsoString());

  public MissingDataLog(
      string resource,
      Period period,
      DateTime nextExtraction,
      int retries,
      bool shouldValidate,
      ErrorDataType error)
  {
    Resource = resource;
    Period = period;
    NextExtraction = nextExtraction;
    Retries = retries;
    ShouldValidate = ShouldValidate;
    ErrorData = error;

    Id = MakeId(Resource, Period);
  }

  [Ignore]
  public string Id { get; init; }

  [Keyword(Name = "resource")]
  public string Resource { get; init; }

  [Object(Name = "period")]
  public Period Period { get; init; }

  [Date(Name = "nextExtraction")]
  public DateTime NextExtraction { get; init; }

  [Number(NumberType.Integer, Name = "retries")]
  public int Retries { get; init; }

  [Boolean(Name = "shouldValidate")]
  public bool ShouldValidate { get; init; }

  [Object(Name = "error")]
  public ErrorDataType ErrorData { get; init; }

  // NOTE: only for elasticsearch purposes
  [Keyword(Name = "type")]
  public string LogType { get; init; } = Type;

  [ElasticsearchType(RelationName = "deviceSourceDeviceData")]
  public class ErrorDataType
  {
    public ErrorDataType(
        string code,
        string description)
    {
      Code = code;
      Description = description;
    }

    [Keyword(Name = "code")]
    public string Code { get; init; }

    [Text(Name = "description")]
    public string Description { get; init; }
  }

  public override bool Equals(object? obj) =>
    Equals(obj as MissingDataLog);

  public bool Equals(MissingDataLog? other) =>
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

  public object Clone() => CloneMissingDataLog();

  public MissingDataLog CloneMissingDataLog() =>
    new MissingDataLog(
      resource: Resource.CloneString(),
      period: Period.ClonePeriod(),
      nextExtraction: NextExtraction,
      retries: Retries,
      shouldValidate: ShouldValidate,
      error:
        new ErrorDataType(
          code: ErrorData.Code.CloneString(),
          description: ErrorData.Description.CloneString()));
};
