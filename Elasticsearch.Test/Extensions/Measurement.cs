using System;
using System.Linq;
using System.Collections.Generic;

namespace Elasticsearch.Test {
  public static class MeasurementExtensions {
    public static (DateTime Min, DateTime Max) GetMeasurementPeriod(
        this IEnumerable<Measurement> measurements) =>
        measurements.Aggregate((Min: DateTime.MaxValue, Max: DateTime.MinValue),
            (period, next) => (Min: (next.MeasurementTimestamp < period.Min
                                         ? next.MeasurementTimestamp
                                         : period.Min),
                Max: (next.MeasurementTimestamp > period.Max
                          ? next.MeasurementTimestamp
                          : period.Min)),
            (period) => (
                Min: period.Min.AddMinutes(-1), Max: period.Max.AddMinutes(1)));
  }
}
