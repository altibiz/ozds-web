using System;
using System.Linq;
using System.Collections.Generic;

namespace Elasticsearch.Test {
  public static class MeasurementExtensions {
    public static Period GetMeasurementPeriod(
        this IEnumerable<Measurement> measurements) =>
        measurements.Aggregate(
            new Period { From = DateTime.MaxValue, To = DateTime.MinValue },
            (period, next) => new Period {
              From = (next.MeasurementTimestamp < period.From
                          ? next.MeasurementTimestamp
                          : period.From),
              To = (next.MeasurementTimestamp > period.To
                        ? next.MeasurementTimestamp
                        : period.To)
            });

    public static Period GetLooseMeasurementPeriod(
        this IEnumerable<Measurement> measurements) {
      var measurementPeriod = measurements.GetMeasurementPeriod();

      return new Period {
        From = measurementPeriod.From.AddMinutes(-1),
        To = measurementPeriod.From.AddMinutes(1),
      };
    }
  }
}
