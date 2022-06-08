using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public readonly record struct MultiDashboardMeasurements
(IEnumerable<string> DeviceIds,
 IEnumerable<MultiDashboardMeasurementData> Measurements);

public readonly record struct MultiDashboardMeasurementData
(DateTime Timestamp,
 IDictionary<string, DashboardMeasurementData> Data);

// TODO: use a list of these instead of a dictionary
public readonly record struct DeviceDashboardMeasurementData
(string DeviceId,
 DashboardMeasurementData Data);

public readonly record struct DashboardMeasurement
(DateTime Timestamp,
 string DeviceId,
 DashboardMeasurementData Data);

public readonly record struct DashboardMeasurementData
(decimal Energy,
 decimal LowCostEnergy,
 decimal HighCostEnergy,
 decimal Power,
 decimal PowerL1,
 decimal PowerL2,
 decimal PowerL3,
 decimal CurrentL1,
 decimal CurrentL2,
 decimal CurrentL3,
 decimal VoltageL1,
 decimal VoltageL2,
 decimal VoltageL3);

public static class DashboardMeasurementExtensions
{
  public static DashboardMeasurement
  ToDashboardMeasurement(
      this Measurement @this) =>
    new DashboardMeasurement
    {
      Timestamp = @this.Timestamp,
      DeviceId = @this.DeviceData.DeviceId,
      Data = @this.ToDashboardMeasurementData(),
    };

  public static DashboardMeasurementData
  ToDashboardMeasurementData(
      this Measurement @this) =>
    new DashboardMeasurementData
    {
      Energy = @this.MeasurementData.energyIn,
      LowCostEnergy = @this.MeasurementData.energyIn_T2,
      HighCostEnergy = @this.MeasurementData.energyIn_T1,
      Power = @this.MeasurementData.powerIn,
      PowerL1 = @this.MeasurementData.currentL1 ?? 0M,
      PowerL2 = @this.MeasurementData.currentL2 ?? 0M,
      PowerL3 = @this.MeasurementData.currentL3 ?? 0M,
      CurrentL1 = @this.MeasurementData.currentL1 ?? 0M,
      CurrentL2 = @this.MeasurementData.currentL2 ?? 0M,
      CurrentL3 = @this.MeasurementData.currentL3 ?? 0M,
      VoltageL1 = @this.MeasurementData.voltageL1 ?? 0M,
      VoltageL2 = @this.MeasurementData.voltageL2 ?? 0M,
      VoltageL3 = @this.MeasurementData.voltageL3 ?? 0M,
    };

  public static MultiDashboardMeasurements
  ToMultiDashboardMeasurements(
      this IEnumerable<Measurement> @this) =>
    @this
      .Select(measurement => measurement.ToDashboardMeasurement())
      .ToMultiDashboardMeasurements();

  public static MultiDashboardMeasurements
  ToMultiDashboardMeasurements(
      this IEnumerable<DashboardMeasurement> @this)
  {
    if (@this.EmptyEnumerable())
    {
      return
        new MultiDashboardMeasurements
        {
          DeviceIds = Enumerable.Empty<string>(),
          Measurements = Enumerable.Empty<MultiDashboardMeasurementData>()
        };
    }

    var deviceGroups = @this
      .OrderBy(measurement => measurement.Timestamp)
      .GroupBy(measurement => measurement.DeviceId);
    var deviceIds = deviceGroups.Select(group => group.Key);

    var events =
      Period
        .Encompassing(@this.Select(measurement => measurement.Timestamp))
        .SplitAscending(deviceGroups.Select(group => group.Count()).Max())
        .Select(period => period.HalfPoint);

    // TODO: optimize
    return
      new MultiDashboardMeasurements
      {
        DeviceIds = deviceIds,
        Measurements = events
          .Select(@event =>
            new MultiDashboardMeasurementData
            {
              Timestamp = @event,
              Data = deviceGroups
                .Select(deviceMeasurements => deviceMeasurements
                  .Interpolate(@event))
                .ToDictionary(
                  pair => pair.Key,
                  pair => pair.Value)
            }),
      };
  }

  private static KeyValuePair<string, DashboardMeasurementData>
  Interpolate(
      this IEnumerable<DashboardMeasurement> @this,
      DateTime at)
  {
    DashboardMeasurement last = default;

    foreach (var current in @this)
    {
      if (current.Timestamp.ToUniversalTime() > at.ToUniversalTime())
      {
        if (last == default)
        {
          return
            new KeyValuePair<string, DashboardMeasurementData>(
              current.DeviceId,
              current.Data);
        }
        else
        {
          var period =
            new Period
            {
              From = last.Timestamp,
              To = current.Timestamp
            };

          return
            new KeyValuePair<string, DashboardMeasurementData>(
              current.DeviceId,
              new DashboardMeasurementData
              {
                Energy = period.Interpolate(
                  last.Data.Energy,
                  current.Data.Energy,
                  at),
                LowCostEnergy = period.Interpolate(
                  last.Data.LowCostEnergy,
                  current.Data.HighCostEnergy,
                  at),
                HighCostEnergy = period.Interpolate(
                  last.Data.HighCostEnergy,
                  current.Data.HighCostEnergy,
                  at),
                Power = period.Interpolate(
                  last.Data.Power,
                  current.Data.Power,
                  at),
                CurrentL1 = period.Interpolate(
                  last.Data.CurrentL1,
                  current.Data.CurrentL1,
                  at),
                CurrentL2 = period.Interpolate(
                  last.Data.CurrentL2,
                  current.Data.CurrentL2,
                  at),
                CurrentL3 = period.Interpolate(
                  last.Data.CurrentL3,
                  current.Data.CurrentL3,
                  at),
                VoltageL1 = period.Interpolate(
                  last.Data.VoltageL1,
                  current.Data.VoltageL1,
                  at),
                VoltageL2 = period.Interpolate(
                  last.Data.VoltageL2,
                  current.Data.VoltageL2,
                  at),
                VoltageL3 = period.Interpolate(
                  last.Data.VoltageL3,
                  current.Data.VoltageL3,
                  at),
              });
        }
      }

      last = current;
    }

    return
      new KeyValuePair<string, DashboardMeasurementData>(
        last.DeviceId,
        last.Data);
  }
}
