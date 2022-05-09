using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IReceiptMeasurementProvider
{
}

public partial class Client : IClient
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)> GetEnergyMeasurementsAsync(
      string deviceId, Period period) =>
    SearchFirstAndLastEnergyMeasurements(deviceId, period)
      .Then(measurements =>
        (measurements.begin.Hits.FirstOrDefault(),
         measurements.end.Hits.FirstOrDefault())
        .FilterNull()
        .WhenNonNullable(measurements =>
          (new EnergyMeasurement
          {
            Energy = measurements.Item1.Source.Data.energyIn,
            HighCostEnergy = measurements.Item1.Source.Data.energyIn_T1,
            LowCostEnergy = measurements.Item1.Source.Data.energyIn_T2,
          },
           new EnergyMeasurement
           {
             Energy = measurements.Item2.Source.Data.energyIn,
             HighCostEnergy = measurements.Item2.Source.Data.energyIn_T1,
             LowCostEnergy = measurements.Item2.Source.Data.energyIn_T2,
           })));

  public (EnergyMeasurement Begin, EnergyMeasurement End) GetEnergyMeasurements(
      string deviceId, Period period) =>
    GetEnergyMeasurementsAsync(deviceId, period).BlockTask();

  public Task<PowerMeasurement> GetPowerMeasurementAsync(
      string deviceId, Period period) =>
    SearchAveragePowerByFifteenMinutes(deviceId, period)
      .Then(measurement => measurement
        .Aggregations
        .AverageBucket("average_power_by_fifteen_minutes")
        .WhenNonNullable(average =>
          new PowerMeasurement((decimal?)average.Value ?? default)));

  public PowerMeasurement GetPowerMeasurement(
      string deviceId, Period period) =>
    GetPowerMeasurementAsync(deviceId, period).BlockTask();

  // NOTE: the first is ascending and second descending
  // NOTE: gte and then lt so we dont mix up first/last measurements
  private Task<(
      ISearchResponse<Measurement> begin,
      ISearchResponse<Measurement> end)>
  SearchFirstAndLastEnergyMeasurements(
      string deviceId, Period period) =>
    (Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(1)
      .Sort(s => s
        .Ascending(f => f.MeasurementTimestamp))
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ??
            DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceId, deviceId))),
     Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(1)
      .Sort(s => s
        .Descending(f => f.MeasurementTimestamp))
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.MeasurementTimestamp)
          .GreaterThanOrEquals(
            period?.From ??
            DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceId, deviceId))))
    .Await();

  // NOTE: https://stackoverflow.com/a/51726136/4348107
  // NOTE: gte and then lt so we dont mix up first/last measurements
  private Task<ISearchResponse<Measurement>>
  SearchAveragePowerByFifteenMinutes(
      string deviceId, Period? period = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
        .Query(q => q
          .DateRange(r => r
            .Field(f => f.MeasurementTimestamp)
            .GreaterThanOrEquals(
              period?.From ??
              DateTime.MinValue.ToUniversalTime())
            .LessThan(
              period?.To ?? DateTime.UtcNow)) && q
          .Term(t => t.DeviceId, deviceId))
        .Aggregations(a => a
          .DateHistogram("timestamp_by_fifteen_minutes", h => h
            .Field(f => f.MeasurementTimestamp)
            .CalendarInterval(TimeSpan.FromMinutes(15))
            .Aggregations(a => a
              .Average("average_power", a => a
                .Field(f => f.Data.powerIn)))))
        .Aggregations(a => a
          .AverageBucket("average_power_by_fifteen_minutes", a => a
            .BucketsPath("timestamp_by_fifteen_minutes>average_power"))));
}
