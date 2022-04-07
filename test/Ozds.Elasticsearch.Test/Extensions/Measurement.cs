using System;
using System.Linq;
using System.Collections.Generic;

namespace Ozds.Elasticsearch.Test
{
  public static class MeasurementExtensions
  {
    public static Period GetMeasurementPeriod(
        this IEnumerable<Measurement> measurements) =>
        measurements.Aggregate(
            new Period
            {
              From = DateTime.MaxValue.ToUniversalTime(),
              To = DateTime.MinValue.ToUniversalTime()
            },
            (period, next) => new Period
            {
              From = (next.MeasurementTimestamp.ToUniversalTime() < period.From
                          ? next.MeasurementTimestamp.ToUniversalTime()
                          : period.From),
              To = (next.MeasurementTimestamp.ToUniversalTime() > period.To
                        ? next.MeasurementTimestamp.ToUniversalTime()
                        : period.To)
            });

    public static Period GetLooseMeasurementPeriod(
        this IEnumerable<Measurement> measurements)
    {
      var measurementPeriod = measurements.GetMeasurementPeriod();

      return new Period
      {
        From = measurementPeriod.From.AddMinutes(-1),
        To = measurementPeriod.From.AddMinutes(1),
      };
    }
  }
}
