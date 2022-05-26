namespace Ozds.Elasticsearch.Test;

public static class MeasurementExtensions
{
  public static Period GetMeasurementPeriod(
      this IEnumerable<Measurement> measurements) =>
    new Period
    {
      From = measurements
        .Select(measurement => measurement.Timestamp.ToUniversalTime())
        .Min(),
      To = measurements
        .Select(measurement => measurement.Timestamp.ToUniversalTime())
        .Max(),
    };

  public static Period GetLooseMeasurementPeriod(
      this IEnumerable<Measurement> measurements)
  {
    var period = measurements.GetMeasurementPeriod();

    return new Period
    {
      From = period.From.AddMinutes(-1),
      To = period.To.AddMinutes(1),
    };
  }

  public static LoadMeasurement ToLoadMeasurement(
      this ExtractionMeasurement measurement) =>
    measurement.ToLoadMeasurement("", "", "", "", "");
}
