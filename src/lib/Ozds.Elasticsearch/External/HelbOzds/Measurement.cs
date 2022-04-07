using System;

// TODO: implement
// NOTE: these fields are like a bare minimum

namespace Ozds.Elasticsearch.HelbOzds {
  public class Measurement {
    public DateTime Timestamp { get; init; } = default!;
    public string DeviceId { get; init; } = default!;
  };
}
