using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IReceiptMeasurementProvider { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(
      string deviceId,
      Period period) =>
    SearchFirstAndLastEnergyMeasurementsAsync(deviceId, period)
      .Then(measurements =>
        (measurements.Begin.Sources().FirstOrDefault(),
         measurements.End.Sources().FirstOrDefault()) switch
        {
          (Measurement begin, Measurement end) =>
          (new EnergyMeasurement
          {
            Energy = begin.MeasurementData.energyIn,
            HighCostEnergy = begin.MeasurementData.energyIn_T1,
            LowCostEnergy = begin.MeasurementData.energyIn_T2,
            Timestamp = begin.Timestamp
          },
            new EnergyMeasurement
            {
              Energy = end.MeasurementData.energyIn,
              HighCostEnergy = end.MeasurementData.energyIn_T1,
              LowCostEnergy = end.MeasurementData.energyIn_T2,
              Timestamp = end.Timestamp
            }),
          _ => default
        });

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(
      string deviceId,
      Period period) =>
    SearchFirstAndLastEnergyMeasurements(deviceId, period)
      .Var(measurements =>
        (measurements.Begin.Sources().FirstOrDefault(),
         measurements.End.Sources().FirstOrDefault()) switch
        {
          (Measurement begin, Measurement end) =>
          (new EnergyMeasurement
          {
            Energy = begin.MeasurementData.energyIn,
            HighCostEnergy = begin.MeasurementData.energyIn_T1,
            LowCostEnergy = begin.MeasurementData.energyIn_T2,
            Timestamp = begin.Timestamp
          },
            new EnergyMeasurement
            {
              Energy = end.MeasurementData.energyIn,
              HighCostEnergy = end.MeasurementData.energyIn_T1,
              LowCostEnergy = end.MeasurementData.energyIn_T2,
              Timestamp = end.Timestamp
            }),
          _ => default
        });

  public Task<PowerMeasurement>
  GetPowerMeasurementAsync(
      string deviceId,
      Period period) =>
    SearchMaxPowerPerFifteenMinutesAsync(
        deviceId,
        period)
      .Then(measurement =>
        new PowerMeasurement
        {
          Power = (decimal)measurement
            .Aggregations
            .MaxBucket("maxPowerPerFifteenMinutes")
            .Value!,
        });

  public PowerMeasurement
  GetPowerMeasurement(
      string deviceId,
      Period period) =>
    SearchMaxPowerPerFifteenMinutes(
        deviceId,
        period)
      .Var(measurement =>
        new PowerMeasurement
        {
          Power = (decimal)measurement
            .Aggregations
            .MaxBucket("maxPowerPerFifteenMinutes")
            .Value!,
        });

  private Task<(
      ISearchResponse<Measurement> Begin,
      ISearchResponse<Measurement> End)>
  SearchFirstAndLastEnergyMeasurementsAsync(
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
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))),
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
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))))
    .Await();

  private (
      ISearchResponse<Measurement> Begin,
      ISearchResponse<Measurement> End)
  SearchFirstAndLastEnergyMeasurements(
      string deviceId,
      Period period) =>
    (Elasticsearch.Search<Measurement>(s => s
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
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))),
     Elasticsearch.Search<Measurement>(s => s
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
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))));

  private ISearchResponse<Measurement>
  SearchMaxPowerPerFifteenMinutes(
      string deviceId,
      Period? period = null) =>
    Elasticsearch.Search<Measurement>(s => s
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
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerFifteenMinutes", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(TimeSpan.FromMinutes(15))
          .Aggregations(a => a
            .Average("averagePower", a => a
              .Field(f => f.MeasurementData.powerIn))))
        .MaxBucket("maxPowerPerFifteenMinutes", a => a
          .BucketsPath("measurementsPerFifteenMinutes>averagePower"))));

  private Task<ISearchResponse<Measurement>>
  SearchMaxPowerPerFifteenMinutesAsync(
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
        .Term(t => t.DeviceData.DeviceId.Suffix("keyword"), deviceId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerFifteenMinutes", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(TimeSpan.FromMinutes(15))
          .Aggregations(a => a
            .Average("averagePower", a => a
              .Field(f => f.MeasurementData.powerIn))))
        .MaxBucket("maxPowerPerFifteenMinutes", a => a
          .BucketsPath("measurementsPerFifteenMinutes>averagePower"))));
}
