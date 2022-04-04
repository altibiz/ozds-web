using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Elasticsearch.MeasurementFaker
{
  public partial interface IClient : IMeasurementProvider { };

  public sealed partial class Client : IClient
  {
    public string Source { get => Client.FakeSource; }

    public IEnumerable<Elasticsearch.Measurement> GetMeasurements(
        Device device, Period? period = null)
    {
      var task = GetElasticsearchMeasurementsAsync(device, period);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsAsync(Device device, Period? period = null)
    {
      return await GetElasticsearchMeasurementsAsync(device, period);
    }

    private async Task<IEnumerable<Elasticsearch.Measurement>>
    GetElasticsearchMeasurementsAsync(Device device, Period? period = null)
    {
      return (await GetNativeMeasurementsAsync(device, period))
          .Select(ConvertMeasurement);
    }

    private Task<List<Measurement>> GetNativeMeasurementsAsync(
        Device device, Period? period = null)
    {
      if (device.SourceDeviceId != Client.FakeDeviceId)
      {
        return Task.FromResult(new List<Measurement>());
      }

      period = period is null ? s_defaultPeriod : period;
      var timeSpan = period.To - period.From;

      if (timeSpan.TotalMinutes < 0)
      {
        timeSpan = s_defaultTimeSpan;
        period = new Period { From = period.To, To = period.To };
      }

      if (timeSpan.TotalMinutes > s_maxTimeSpanMinutes)
      {
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
      foreach (var _ in Enumerable.Range(0, (measurementCount)))
      {
        result.Add(Measurement.Generate(currentMeasurementTimestamp));
        currentMeasurementTimestamp = currentMeasurementTimestamp.AddSeconds(
            60 / s_measurementsPerMinute);
      }

      return Task.FromResult(result);
    }

    private Elasticsearch.Measurement ConvertMeasurement(
        Measurement measurement) =>
      new Elasticsearch.Measurement(measurement.Timestamp,
          null, Source,
          Elasticsearch.Device.MakeId(Source, measurement.DeviceId),
          new Elasticsearch.Measurement.KnownData
          {
            dongleId = measurement.Data.dongleId,
            meterIdent = measurement.Data.meterIdent,
            meterSerial = measurement.Data.meterSerial,
            localTime = measurement.Data.localTime,
            localDate = measurement.Data.localDate,
            tariff = measurement.Data.tariff,
            limiter = measurement.Data.limiter,
            fuseSupervisionL1 = measurement.Data.fuseSupervisionL1,
            disconnectControl = measurement.Data.disconnectControl,
            numLongPwrFailures = measurement.Data.numLongPwrFailures,
            numPwrFailures = measurement.Data.numPwrFailures,
            numVoltageSagsL1 = measurement.Data.numVoltageSagsL1,
            numVoltageSagsL2 = measurement.Data.numVoltageSagsL2,
            numVoltageSagsL3 = measurement.Data.numVoltageSagsL3,
            numVoltageSwellsL1 = measurement.Data.numVoltageSwellsL1,
            numVoltageSwellsL2 = measurement.Data.numVoltageSwellsL2,
            numVoltageSwellsL3 = measurement.Data.numVoltageSwellsL3,
            currentL1 = measurement.Data.currentL1,
            currentL2 = measurement.Data.currentL2,
            currentL3 = measurement.Data.currentL3,
            energyIn = measurement.Data.energyIn,
            energyIn_T1 = measurement.Data.energyIn_T1,
            energyIn_T2 = measurement.Data.energyIn_T2,
            energyOut = measurement.Data.energyOut,
            energyOut_T1 = measurement.Data.energyOut_T1,
            energyOut_T2 = measurement.Data.energyOut_T2,
            powerIn = measurement.Data.powerIn,
            powerInL1 = measurement.Data.powerInL1,
            powerInL2 = measurement.Data.powerInL2,
            powerInL3 = measurement.Data.powerInL3,
            powerOut = measurement.Data.powerOut,
            powerOutL1 = measurement.Data.powerOutL1,
            powerOutL2 = measurement.Data.powerOutL2,
            powerOutL3 = measurement.Data.powerOutL3,
            voltageL1 = measurement.Data.voltageL1,
            voltageL2 = measurement.Data.voltageL2,
            voltageL3 = measurement.Data.voltageL3,
          });

    private static int s_measurementsPerMinute = 4;

    private static int s_maxTimeSpanMinutes = 1000;

    private static Period s_defaultPeriod =
        new Period
        {
          From = DateTime.UtcNow.AddMinutes(-5),
          To = DateTime.UtcNow
        };

    private static TimeSpan s_defaultTimeSpan = TimeSpan.FromMinutes(5);
  }
}
