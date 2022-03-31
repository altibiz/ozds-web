using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Elasticsearch.MeasurementFaker {
  public partial interface IClient : IMeasurementProvider {};

  public sealed partial class Client : IClient {
    public string Source { get => Client.FakeSource; }

    public IEnumerable<Elasticsearch.Measurement> GetMeasurements(
        Device device, Period? period = null) {
      var task = GetElasticsearchMeasurementsAsync(device, period);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsAsync(Device device, Period? period = null) {
      return await GetElasticsearchMeasurementsAsync(device, period);
    }

    private async Task<IEnumerable<Elasticsearch.Measurement>>
    GetElasticsearchMeasurementsAsync(Device device, Period? period = null) {
      return (await GetNativeMeasurementsAsync(device, period))
          .Select(ConvertMeasurement);
    }

    private Task<List<Measurement>> GetNativeMeasurementsAsync(
        Device device, Period? period = null) {
      if (device.SourceDeviceId != Client.FakeDeviceId) {
        return Task.FromResult(new List<Measurement>());
      }

      var result = new List<Measurement>();
      var measurementCount =
          period == null ? 20 : (period.To - period.From).TotalMinutes * 4;
      var currentMeasurementTimestamp =
          period == null ? DateTime.UtcNow.AddMinutes(-5) : period.From;
      foreach (var _ in Enumerable.Range(0, ((int)measurementCount))) {
        result.Add(new Measurement(currentMeasurementTimestamp));
        currentMeasurementTimestamp =
            currentMeasurementTimestamp.AddSeconds(15);
      }

      Logger.LogDebug($"Faked {measurementCount} measurements for {device.Id}");

      return Task.FromResult(result);
    }

    private Elasticsearch.Measurement ConvertMeasurement(
        Measurement measurement) {
      return new Elasticsearch.Measurement(measurement.Timestamp, null, Source,
          Elasticsearch.Device.MakeId(Source, measurement.DeviceId));
    }
  }
}
