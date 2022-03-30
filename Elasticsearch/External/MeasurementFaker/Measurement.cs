using System;

namespace Elasticsearch.MeasurementFaker {
  public class Measurement {
    public Measurement(DateTime? timestamp = null) {
      Timestamp = timestamp ?? DateTime.UtcNow.AddMinutes(-1);
    }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow.AddMinutes(-1);
    public string DeviceId { get; init; } = Client.FakeDeviceId;
  };
}
