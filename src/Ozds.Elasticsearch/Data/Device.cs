using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

public class DeviceState
{
  public const string Added = "added";
  public const string Active = "healthy";
  public const string TemporarilyInactive = "unhealthy";
  public const string Inactive = "inactive";
  public const string Removed = "removed";
}

[ElasticsearchType(RelationName = "device", IdProperty = nameof(Id))]
public class Device
{
  public static string MakeId(
      string source,
      string sourceDeviceId) =>
    Strings.CombineIntoStringId(
        "S", source.Substring(0, 3), "ID", sourceDeviceId);

  public Device(
      string source,
      string sourceDeviceId,
      KnownSourceDeviceData? sourceDeviceData = null,
      string? state = null)
  {
    Source = source;
    SourceDeviceId = sourceDeviceId;
    SourceDeviceData = sourceDeviceData ?? new KnownSourceDeviceData { };
    State = state ?? DeviceState.Added;
    Id = MakeId(Source, SourceDeviceId);
  }

  public string Id { get; init; }

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Keyword(Name = "sourceDeviceId")]
  public string SourceDeviceId { get; init; }

  [Object(Name = "sourceDeviceData")]
  public KnownSourceDeviceData SourceDeviceData { get; init; } =
    new KnownSourceDeviceData { };

  [Keyword(Name = "state")]
  public string State { get; init; }

  [Date(Name = "dateAdded")]
  public DateTime DateAdded { get; init; } = DateTime.UtcNow;

  [Date(Name = "dateDiscontinued")]
  public DateTime? DateDiscontinued { get; init; } = null;

  [ElasticsearchType(RelationName = "deviceSourceData")]
  public class KnownSourceDeviceData
  {
    [Keyword(Name = "ownerId")]
    public string? ownerId { get; init; } = default;
  }

  public override bool Equals(object? obj) =>
    Equals(obj as Device);

  public bool Equals(Device? other) =>
    other != null &&
    SourceDeviceId == other.SourceDeviceId &&
    Source == other.Source;

  public override int GetHashCode() =>
    HashCode.Combine(SourceDeviceId, Source);
}
