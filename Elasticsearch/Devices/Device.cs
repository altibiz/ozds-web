using System;
using Nest;

namespace Elasticsearch {
public class DeviceState {
  public const string Added = "added";
  public const string Discontinued = "discontinued";

  public const string Healthy = "healthy";
  public const string Unhealthy = "unhealthy";
}

[ElasticsearchType(RelationName = "device")]
public class Device {
  [Keyword(Name = "deviceId")]
  public string DeviceId { get; init; } = default!;

  [Keyword(Name = "source")]
  public string Source { get; init; } = default!;

  [Object(Name = "sourceData")]
  public KnownSourceData SourceData { get; init; } = default!;

  [Keyword(Name = "state")]
  public string State { get; init; } = DeviceState.Added;

  [Date(Name = "dateAdded")]
  public DateTime DateAdded { get; init; } = default!;

  [Date(Name = "dateDiscontinued")]
  public DateTime? DateDiscontinued { get; init; } = default;

  public override bool Equals(object? obj) { return Equals(obj as Device); }

  public bool Equals(Device? other) {
    return other != null && DeviceId == other.DeviceId &&
           Source == other.Source;
  }

  public override int GetHashCode() {
    return HashCode.Combine(DeviceId, Source);
  }

  [ElasticsearchType(RelationName = "deviceSourceData")]
  public class KnownSourceData {
    [Keyword(Name = "ownerId")]
    public string? ownerId { get; init; } = default;
  }
}
}
