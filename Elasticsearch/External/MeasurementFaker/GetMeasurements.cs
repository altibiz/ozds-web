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

      period = period is null ? s_defaultPeriod : period;
      var timeSpan = period.To - period.From;

      if (timeSpan.TotalMinutes < 0) {
        timeSpan = s_defaultTimeSpan;
        period = new Period { From = period.To, To = period.To };
      }

      if (timeSpan.TotalMinutes > s_maxTimeSpanMinutes) {
        timeSpan = s_defaultTimeSpan;
        period =
            new Period { From = period.To.Subtract(timeSpan), To = period.To };
      }

      var measurementCount =
          (int)(timeSpan.TotalMinutes * s_measurementsPerMinute) - 1;
      var currentMeasurementTimestamp = period.From.AddSeconds(1);
      Logger.LogDebug($"Faking {measurementCount} measurements " +
                      $"for {device.Id} " + $"in {period}");

      var result = new List<Measurement>();
      foreach (var _ in Enumerable.Range(0, (measurementCount))) {
        result.Add(new Measurement(currentMeasurementTimestamp));
        currentMeasurementTimestamp = currentMeasurementTimestamp.AddSeconds(
            60 / s_measurementsPerMinute);
      }

      return Task.FromResult(result);
    }

    private Elasticsearch.Measurement ConvertMeasurement(
        Measurement measurement) {
      return new Elasticsearch.Measurement(measurement.Timestamp, null, Source,
          Elasticsearch.Device.MakeId(Source, measurement.DeviceId));
    }

    private static int s_measurementsPerMinute = 4;

    private static int s_maxTimeSpanMinutes = 1000;

    private static Period s_defaultPeriod =
        new Period { From = DateTime.UtcNow.AddMinutes(-5),
          To = DateTime.UtcNow };

    private static TimeSpan s_defaultTimeSpan = TimeSpan.FromMinutes(5);
  }
}
