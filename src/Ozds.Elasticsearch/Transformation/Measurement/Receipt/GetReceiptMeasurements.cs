using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IReceiptMeasurementProvider { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(
      string source,
      string deviceId,
      Period period) =>
    SearchFirstAndLastEnergyMeasurements(source, deviceId, period)
      .Then(measurements =>
        (measurements.begin.Hits.FirstOrDefault(),
         measurements.end.Hits.FirstOrDefault())
        .FilterNull()
        .WhenNonNull(measurements =>
          (new EnergyMeasurement
          {
            Energy =
              measurements.Item1.Source.MeasurementData.energyIn,
            HighCostEnergy =
              measurements.Item1.Source.MeasurementData.energyIn_T1,
            LowCostEnergy =
              measurements.Item1.Source.MeasurementData.energyIn_T2,
            Timestamp =
              measurements.Item1.Source.Timestamp
          },
           new EnergyMeasurement
           {
             Energy =
              measurements.Item2.Source.MeasurementData.energyIn,
             HighCostEnergy =
              measurements.Item2.Source.MeasurementData.energyIn_T1,
             LowCostEnergy =
              measurements.Item2.Source.MeasurementData.energyIn_T2,
             Timestamp =
              measurements.Item2.Source.Timestamp
           })));

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(
      string source,
      string deviceId,
      Period period) =>
    GetEnergyMeasurementsAsync(
        source,
        deviceId,
        period).Block();

  public Task<PowerMeasurement>
  GetPowerMeasurementAsync(
      string source,
      string deviceId,
      Period period) =>
    SearchAveragePowerByFifteenMinutes(
        source,
        deviceId,
        period)
      .Then(measurement => measurement
        .Aggregations
        .AverageBucket("averagePowerByFifteenMinutes")
        .WhenNonNull(average =>
          new PowerMeasurement((decimal?)average.Value ?? default)));

  public PowerMeasurement
  GetPowerMeasurement(
      string source,
      string deviceId,
      Period period) =>
    GetPowerMeasurementAsync(
        source,
        deviceId,
        period).Block();

  // NOTE: the first is ascending and second descending
  // NOTE: gte and then lt so we dont mix up first/last measurements
  private Task<(
      ISearchResponse<Measurement> begin,
      ISearchResponse<Measurement> end)>
  SearchFirstAndLastEnergyMeasurements(
      string source,
      string deviceId,
      Period period) =>
    (Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(1)
      .Index(MeasurementIndexName)
      .Sort(s => s
        .Ascending(f => f.Timestamp))
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ??
            DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceData.DeviceId, Device.MakeId(source, deviceId)))),
     Elasticsearch.SearchAsync<Measurement>(s => s
      .Size(1)
      .Index(MeasurementIndexName)
      .Sort(s => s
        .Descending(f => f.Timestamp))
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ??
            DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceData.DeviceId, Device.MakeId(source, deviceId)))))
    .Await();

  // NOTE: https://stackoverflow.com/a/51726136/4348107
  // NOTE: gte and then lt so we dont mix up first/last measurements
  private Task<ISearchResponse<Measurement>>
  SearchAveragePowerByFifteenMinutes(
      string source,
      string deviceId,
      Period? period = null) =>
    Elasticsearch.SearchAsync<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .Query(q => q
        .DateRange(r => r
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(
            period?.From ??
            DateTime.MinValue.ToUniversalTime())
          .LessThan(
            period?.To ?? DateTime.UtcNow)) && q
        .Term(t => t.DeviceData.DeviceId, Device.MakeId(source, deviceId)))
      .Aggregations(a => a
        .DateHistogram("timestampByFifteenMinutes", h => h
          .Field(f => f.Timestamp)
          .CalendarInterval(TimeSpan.FromMinutes(15))
          .Aggregations(a => a
            .Average("averagePower", a => a
              .Field(f => f.MeasurementData.powerIn)))))
      .Aggregations(a => a
        .AverageBucket("averagePowerByFifteenMinutes", a => a
          .BucketsPath("timestampByFifteenMinutes>averagePower"))));
}
