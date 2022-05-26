namespace Ozds.Elasticsearch.Test;

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
            From = (next.Timestamp.ToUniversalTime() < period.From
                        ? next.Timestamp.ToUniversalTime()
                        : period.From),
            To = (next.Timestamp.ToUniversalTime() > period.To
                      ? next.Timestamp.ToUniversalTime()
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

  public static LoadMeasurement ToLoadMeasurement(
      this ExtractionMeasurement measurement) =>
    measurement.ToLoadMeasurement(
      "",
      "",
      "",
      "",
      "");
}
