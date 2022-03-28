using System;
using Nest;

namespace Elasticsearch {
public class DeviceState {
  public const string Added = "added";
  public const string Discontinued = "discontinued";

  public const string Healthy = "healthy";
  public const string Unhealthy = "unhealthy";
}

[ElasticsearchType(RelationName = "device", IdProperty = nameof(Id))]
public class Device {
  public static string MakeId(string source, string sourceDeviceId) {
    return StringExtensions.CombineIntoStringId(
        "S", source.Substring(0, 3), "ID", sourceDeviceId);
  }

  public Device(string source, string sourceDeviceId,
      KnownSourceDeviceData? sourceDeviceData = null, string? state = null) {
    Source = source;
    SourceDeviceId = sourceDeviceId;
    SourceDeviceData = sourceDeviceData ?? new KnownSourceDeviceData {};
    State = state ?? DeviceState.Added;
    Id = MakeId(Source, SourceDeviceId);
  }

  public string Id { get; init; }

  [Keyword(Name = "source")]
  public string Source { get; init; }

  [Keyword(Name = "sourceDeviceId")]
  public string SourceDeviceId { get; init; }

  [Object(Name = "sourceDeviceData")]
  public KnownSourceDeviceData SourceDeviceData { get; init; } = new KnownSourceDeviceData{};

  [Keyword(Name = "state")]
  public string State { get; init; }

  [Date(Name = "dateAdded")]
  public DateTime DateAdded { get; init; } = DateTime.Now;

  [Date(Name = "dateDiscontinued")]
  public DateTime? DateDiscontinued { get; init; } = null;

  public override bool Equals(object? obj) { return Equals(obj as Device); }

  public bool Equals(Device? other) {
    return other != null && SourceDeviceId == other.SourceDeviceId &&
           Source == other.Source;
  }

  public override int GetHashCode() {
    return HashCode.Combine(SourceDeviceId, Source);
  }

  [ElasticsearchType(RelationName = "deviceSourceData")]
  public class KnownSourceDeviceData {
    [Keyword(Name = "ownerId")]
    public string? ownerId { get; init; } = default;
  }
}
}
