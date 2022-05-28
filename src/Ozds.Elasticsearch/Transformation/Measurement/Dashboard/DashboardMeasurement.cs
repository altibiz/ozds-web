using Ozds.Util;

namespace Ozds.Elasticsearch;

public readonly record struct MultiDashboardMeasurements
(IEnumerable<string> DeviceIds,
 IEnumerable<MultiDashboardMeasurementData> Measurements);

public readonly record struct MultiDashboardMeasurementData
(DateTime Timestamp,
 IDictionary<string, DashboardMeasurementData> Data);

public readonly record struct DashboardMeasurement
(DateTime Timestamp,
 string DeviceId,
 DashboardMeasurementData Data);

public readonly record struct DashboardMeasurementData
(decimal Energy,
 decimal LowCostEnergy,
 decimal HighCostEnergy,
 decimal Power);

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
      Power = @this.MeasurementData.powerIn
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
      return default;
    }

    var deviceGroups = @this.GroupBy(measurement => measurement.DeviceId);
    var deviceIds = deviceGroups.Select(group => group.Key);

    var events =
      Period
        .Encompassing(@this.Select(measurement => measurement.Timestamp))
        .SplitAscending(deviceGroups.Select(group => group.Count()).Max())
        .Select(period => period.Interpolation);

    // TODO: optimize
    return
      new MultiDashboardMeasurements
      {
        DeviceIds = deviceIds,
        Measurements =
            events.Select(@event =>
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
      if (current.Timestamp > at)
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
                Power = period.Interpolate(
                  last.Data.Power,
                  current.Data.Power,
                  at),
                Energy = period.Interpolate(
                  last.Data.Energy,
                  current.Data.Energy,
                  at),
                LowCostEnergy = period.Interpolate(
                  last.Data.LowCostEnergy,
                  last.Data.HighCostEnergy,
                  at),
                HighCostEnergy = period.Interpolate(
                  last.Data.HighCostEnergy,
                  last.Data.HighCostEnergy,
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
