using Nest;

using Ozds.Extensions;

namespace Ozds.Elasticsearch;

// TODO: shorten this thing
// TODO: filter nulls

public partial interface IElasticsearchClient : IDashboardMeasurementProvider { }

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public const int MaxMeasurementsPerResponse = 400;
  public static readonly TimeSpan MaxPeriodSpanForShortPeriod =
    TimeSpan.FromHours(1);
  public static readonly TimeSpan DefaultInterval = TimeSpan.FromDays(30);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsAsync(
      string deviceId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null || longPeriod.Span > MaxPeriodSpanForShortPeriod =>
        SearchAverageDashboardMeasurementsPerIntervalAsync(
          deviceId,
          longPeriod,
          longPeriod switch
          {
            Period exactPeriod => exactPeriod.Span / MaxMeasurementsPerResponse,
            null => DefaultInterval
          })
          .Then(response => response.Aggregations
            .DateHistogram("measurementsPerInterval").Buckets
            .Select(bucket =>
              new DashboardMeasurement
              {
                Timestamp = bucket.Date,
                DeviceId = deviceId,
                Data =
                  new DashboardMeasurementData
                  {
                    Energy = (decimal?)bucket
                      .AverageBucket("averageEnergy").Value ?? default,
                    LowCostEnergy = (decimal?)bucket
                      .AverageBucket("averageLowCostEnergy").Value ?? default,
                    HighCostEnergy = (decimal?)bucket
                      .AverageBucket("averageHighCostEnergy").Value ?? default,
                    Power = (decimal?)bucket
                      .AverageBucket("averagePower").Value ?? default,
                    PowerL1 = (decimal?)bucket
                      .AverageBucket("averagePowerL1").Value ?? default,
                    PowerL2 = (decimal?)bucket
                      .AverageBucket("averagePowerL2").Value ?? default,
                    PowerL3 = (decimal?)bucket
                      .AverageBucket("averagePowerL3").Value ?? default,
                    CurrentL1 = (decimal?)bucket
                      .AverageBucket("averageCurrentL1").Value ?? default,
                    CurrentL2 = (decimal?)bucket
                      .AverageBucket("averageCurrentL2").Value ?? default,
                    CurrentL3 = (decimal?)bucket
                      .AverageBucket("averageCurrentL3").Value ?? default,
                    VoltageL1 = (decimal?)bucket
                      .AverageBucket("averageVoltageL1").Value ?? default,
                    VoltageL2 = (decimal?)bucket
                      .AverageBucket("averageVoltageL2").Value ?? default,
                    VoltageL3 = (decimal?)bucket
                      .AverageBucket("averageVoltageL3").Value ?? default,
                  }
              }
              )),
      (var shortPeriod) =>
        SearchMeasurementsByDeviceAsync(deviceId, shortPeriod)
          .Then(measurements => measurements
            .Sources()
            .Select(measurement => measurement
              .ToDashboardMeasurement()))
    };

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurements(
      string deviceId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null || longPeriod.Span > MaxPeriodSpanForShortPeriod =>
        SearchAverageDashboardMeasurementsPerInterval(
          deviceId,
          longPeriod,
          longPeriod switch
          {
            Period exactPeriod => exactPeriod.Span / MaxMeasurementsPerResponse,
            null => DefaultInterval
          })
          .Var(response => response.Aggregations
            .DateHistogram("measurementsPerInterval").Buckets
            .Select(bucket =>
              new DashboardMeasurement
              {
                Timestamp = bucket.Date,
                DeviceId = deviceId,
                Data =
                  new DashboardMeasurementData
                  {
                    Energy = (decimal?)bucket
                      .AverageBucket("averageEnergy").Value ?? default,
                    LowCostEnergy = (decimal?)bucket
                      .AverageBucket("averageLowCostEnergy").Value ?? default,
                    HighCostEnergy = (decimal?)bucket
                      .AverageBucket("averageHighCostEnergy").Value ?? default,
                    Power = (decimal?)bucket
                      .AverageBucket("averagePower").Value ?? default,
                    PowerL1 = (decimal?)bucket
                      .AverageBucket("averagePowerL1").Value ?? default,
                    PowerL2 = (decimal?)bucket
                      .AverageBucket("averagePowerL2").Value ?? default,
                    PowerL3 = (decimal?)bucket
                      .AverageBucket("averagePowerL3").Value ?? default,
                    CurrentL1 = (decimal?)bucket
                      .AverageBucket("averageCurrentL1").Value ?? default,
                    CurrentL2 = (decimal?)bucket
                      .AverageBucket("averageCurrentL2").Value ?? default,
                    CurrentL3 = (decimal?)bucket
                      .AverageBucket("averageCurrentL3").Value ?? default,
                    VoltageL1 = (decimal?)bucket
                      .AverageBucket("averageVoltageL1").Value ?? default,
                    VoltageL2 = (decimal?)bucket
                      .AverageBucket("averageVoltageL2").Value ?? default,
                    VoltageL3 = (decimal?)bucket
                      .AverageBucket("averageVoltageL3").Value ?? default,
                  }
              }
              )),
      (var shortPeriod) =>
        SearchMeasurementsByDevice(deviceId, shortPeriod)
          .Var(measurements => measurements
            .Sources()
            .Select(measurement => measurement
              .ToDashboardMeasurement()))
    };

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null || longPeriod.Span > MaxPeriodSpanForShortPeriod =>
        SearchAverageDashboardMeasurementsByOwnerPerIntervalAsync(
          ownerId,
          longPeriod,
          longPeriod switch
          {
            Period exactPeriod => exactPeriod.Span / MaxMeasurementsPerResponse,
            null => DefaultInterval
          })
          .Then(response =>
            new MultiDashboardMeasurements
            {
              DeviceIds = response.Aggregations
                .DateHistogram("measurementsPerInterval").Buckets
                .SelectMany(bucket => bucket
                  .Terms("measurementsByDeviceId").Buckets
                  .Select(bucket => bucket.Key))
                .Unique(),
              Measurements =
                response.Aggregations
                  .DateHistogram("measurementsPerInterval").Buckets
                  .Select(bucket =>
                    new MultiDashboardMeasurementData
                    {
                      Timestamp = bucket.Date,
                      Data = bucket
                        .Terms("measurementsByDeviceId").Buckets
                        .Select(bucket =>
                          new DeviceDashboardMeasurementData(
                            bucket.Key,
                            new DashboardMeasurementData
                            {
                              Energy = (decimal?)bucket
                                .AverageBucket("averageEnergy").Value ?? default,
                              LowCostEnergy = (decimal?)bucket
                                .AverageBucket("averageLowCostEnergy").Value ?? default,
                              HighCostEnergy = (decimal?)bucket
                                .AverageBucket("averageHighCostEnergy").Value ?? default,
                              Power = (decimal?)bucket
                                .AverageBucket("averagePower").Value ?? default,
                              PowerL1 = (decimal?)bucket
                                .AverageBucket("averagePowerL1").Value ?? default,
                              PowerL2 = (decimal?)bucket
                                .AverageBucket("averagePowerL2").Value ?? default,
                              PowerL3 = (decimal?)bucket
                                .AverageBucket("averagePowerL3").Value ?? default,
                              CurrentL1 = (decimal?)bucket
                                .AverageBucket("averageCurrentL1").Value ?? default,
                              CurrentL2 = (decimal?)bucket
                                .AverageBucket("averageCurrentL2").Value ?? default,
                              CurrentL3 = (decimal?)bucket
                                .AverageBucket("averageCurrentL3").Value ?? default,
                              VoltageL1 = (decimal?)bucket
                                .AverageBucket("averageVoltageL1").Value ?? default,
                              VoltageL2 = (decimal?)bucket
                                .AverageBucket("averageVoltageL2").Value ?? default,
                              VoltageL3 = (decimal?)bucket
                                .AverageBucket("averageVoltageL3").Value ?? default,
                            })),
                    }),
            }),
      (var shortPeriod) =>
        SearchMeasurementsByOwnerSortedAsync(ownerId, shortPeriod)
          .Then(response => response
            .Sources()
            .ToMultiDashboardMeasurements())
    };

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null || longPeriod.Span > MaxPeriodSpanForShortPeriod =>
        SearchAverageDashboardMeasurementsByOwnerPerInterval(
          ownerId,
          longPeriod,
          longPeriod switch
          {
            Period exactPeriod => exactPeriod.Span / MaxMeasurementsPerResponse,
            null => DefaultInterval
          })
          .Var(response =>
            new MultiDashboardMeasurements
            {
              DeviceIds = response.Aggregations
                .DateHistogram("measurementsPerInterval").Buckets
                .SelectMany(bucket => bucket
                  .Terms("measurementsByDeviceId").Buckets
                  .Select(bucket => bucket.Key))
                .Unique(),
              Measurements =
                response.Aggregations
                  .DateHistogram("measurementsPerInterval").Buckets
                  .Select(bucket =>
                    new MultiDashboardMeasurementData
                    {
                      Timestamp = bucket.Date,
                      Data = bucket
                        .Terms("measurementsByDeviceId").Buckets
                        .Select(bucket =>
                          new DeviceDashboardMeasurementData(
                            bucket.Key,
                            new DashboardMeasurementData
                            {
                              Energy = (decimal?)bucket
                                .AverageBucket("averageEnergy").Value ?? default,
                              LowCostEnergy = (decimal?)bucket
                                .AverageBucket("averageLowCostEnergy").Value ?? default,
                              HighCostEnergy = (decimal?)bucket
                                .AverageBucket("averageHighCostEnergy").Value ?? default,
                              Power = (decimal?)bucket
                                .AverageBucket("averagePower").Value ?? default,
                              PowerL1 = (decimal?)bucket
                                .AverageBucket("averagePowerL1").Value ?? default,
                              PowerL2 = (decimal?)bucket
                                .AverageBucket("averagePowerL2").Value ?? default,
                              PowerL3 = (decimal?)bucket
                                .AverageBucket("averagePowerL3").Value ?? default,
                              CurrentL1 = (decimal?)bucket
                                .AverageBucket("averageCurrentL1").Value ?? default,
                              CurrentL2 = (decimal?)bucket
                                .AverageBucket("averageCurrentL2").Value ?? default,
                              CurrentL3 = (decimal?)bucket
                                .AverageBucket("averageCurrentL3").Value ?? default,
                              VoltageL1 = (decimal?)bucket
                                .AverageBucket("averageVoltageL1").Value ?? default,
                              VoltageL2 = (decimal?)bucket
                                .AverageBucket("averageVoltageL2").Value ?? default,
                              VoltageL3 = (decimal?)bucket
                                .AverageBucket("averageVoltageL3").Value ?? default,
                            })),
                    }),
            }),
      (var shortPeriod) =>
        SearchMeasurementsByOwnerSorted(ownerId, shortPeriod)
          .Var(response => response
            .Sources()
            .ToMultiDashboardMeasurements())
    };

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null || longPeriod.Span > MaxPeriodSpanForShortPeriod =>
        SearchAverageDashboardMeasurementsByOwnerUserPerIntervalAsync(
          ownerUserId,
          longPeriod,
          longPeriod switch
          {
            Period exactPeriod => exactPeriod.Span / MaxMeasurementsPerResponse,
            null => DefaultInterval
          })
          .Then(response =>
            new MultiDashboardMeasurements
            {
              DeviceIds = response.Aggregations
                .DateHistogram("measurementsPerInterval").Buckets
                .SelectMany(bucket => bucket
                  .Terms("measurementsByDeviceId").Buckets
                  .Select(bucket => bucket.Key))
                .Unique(),
              Measurements =
                response.Aggregations
                  .DateHistogram("measurementsPerInterval").Buckets
                  .Select(bucket =>
                    new MultiDashboardMeasurementData
                    {
                      Timestamp = bucket.Date,
                      Data = bucket
                        .Terms("measurementsByDeviceId").Buckets
                        .Select(bucket =>
                          new DeviceDashboardMeasurementData(
                            bucket.Key,
                            new DashboardMeasurementData
                            {
                              Energy = (decimal?)bucket
                                .AverageBucket("averageEnergy")
                                .Value ?? default,
                              LowCostEnergy = (decimal?)bucket
                                .AverageBucket("averageLowCostEnergy")
                                .Value ?? default,
                              HighCostEnergy = (decimal?)bucket
                                .AverageBucket("averageHighCostEnergy")
                                .Value ?? default,
                              Power = (decimal?)bucket
                                .AverageBucket("averagePower")
                                .Value ?? default,
                              PowerL1 = (decimal?)bucket
                                .AverageBucket("averagePowerL1")
                                .Value ?? default,
                              PowerL2 = (decimal?)bucket
                                .AverageBucket("averagePowerL2")
                                .Value ?? default,
                              PowerL3 = (decimal?)bucket
                                .AverageBucket("averagePowerL3")
                                .Value ?? default,
                              CurrentL1 = (decimal?)bucket
                                .AverageBucket("averageCurrentL1")
                                .Value ?? default,
                              CurrentL2 = (decimal?)bucket
                                .AverageBucket("averageCurrentL2")
                                .Value ?? default,
                              CurrentL3 = (decimal?)bucket
                                .AverageBucket("averageCurrentL3")
                                .Value ?? default,
                              VoltageL1 = (decimal?)bucket
                                .AverageBucket("averageVoltageL1")
                                .Value ?? default,
                              VoltageL2 = (decimal?)bucket
                                .AverageBucket("averageVoltageL2")
                                .Value ?? default,
                              VoltageL3 = (decimal?)bucket
                                .AverageBucket("averageVoltageL3")
                                .Value ?? default,
                            })),
                    }),
            }),
      (var shortPeriod) =>
        SearchMeasurementsByOwnerUserSortedAsync(ownerUserId, shortPeriod)
          .Then(response => response
            .Sources()
            .ToMultiDashboardMeasurements())
    };

  public MultiDashboardMeasurements
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null || longPeriod.Span > MaxPeriodSpanForShortPeriod =>
        SearchAverageDashboardMeasurementsByOwnerUserPerInterval(
          ownerUserId,
          longPeriod,
          longPeriod switch
          {
            Period exactPeriod => exactPeriod.Span / MaxMeasurementsPerResponse,
            null => DefaultInterval
          })
          .Var(response =>
            new MultiDashboardMeasurements
            {
              DeviceIds = response.Aggregations
                .DateHistogram("measurementsPerInterval").Buckets
                .SelectMany(bucket => bucket
                  .Terms("measurementsByDeviceId").Buckets
                  .Select(bucket => bucket.Key))
                .Unique(),
              Measurements =
                response.Aggregations
                  .DateHistogram("measurementsPerInterval").Buckets
                  .Select(bucket =>
                    new MultiDashboardMeasurementData
                    {
                      Timestamp = bucket.Date,
                      Data = bucket
                        .Terms("measurementsByDeviceId").Buckets
                        .Select(bucket =>
                          new DeviceDashboardMeasurementData(
                            bucket.Key,
                            new DashboardMeasurementData
                            {
                              Energy = (decimal?)bucket
                                .AverageBucket("averageEnergy")
                                .Value ?? default,
                              LowCostEnergy = (decimal?)bucket
                                .AverageBucket("averageLowCostEnergy")
                                .Value ?? default,
                              HighCostEnergy = (decimal?)bucket
                                .AverageBucket("averageHighCostEnergy")
                                .Value ?? default,
                              Power = (decimal?)bucket
                                .AverageBucket("averagePower")
                                .Value ?? default,
                              PowerL1 = (decimal?)bucket
                                .AverageBucket("averagePowerL1")
                                .Value ?? default,
                              PowerL2 = (decimal?)bucket
                                .AverageBucket("averagePowerL2")
                                .Value ?? default,
                              PowerL3 = (decimal?)bucket
                                .AverageBucket("averagePowerL3")
                                .Value ?? default,
                              CurrentL1 = (decimal?)bucket
                                .AverageBucket("averageCurrentL1")
                                .Value ?? default,
                              CurrentL2 = (decimal?)bucket
                                .AverageBucket("averageCurrentL2")
                                .Value ?? default,
                              CurrentL3 = (decimal?)bucket
                                .AverageBucket("averageCurrentL3")
                                .Value ?? default,
                              VoltageL1 = (decimal?)bucket
                                .AverageBucket("averageVoltageL1")
                                .Value ?? default,
                              VoltageL2 = (decimal?)bucket
                                .AverageBucket("averageVoltageL2")
                                .Value ?? default,
                              VoltageL3 = (decimal?)bucket
                                .AverageBucket("averageVoltageL3")
                                .Value ?? default,
                            })),
                    }),
            }),
      (var shortPeriod) =>
        SearchMeasurementsByOwnerUserSorted(ownerUserId, shortPeriod)
          .Var(response => response
            .Sources()
            .ToMultiDashboardMeasurements())
    };

  private Task<ISearchResponse<Measurement>>
  SearchAverageDashboardMeasurementsByOwnerUserPerIntervalAsync(
      string ownerUserId,
      Period? period,
      TimeSpan interval) =>
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
        .Term(t => t.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Terms("measurementsByDeviceId", t => t
              .Field(f => f.DeviceData.DeviceId.Suffix("keyword"))
              .Aggregations(a => a
                .Average("averageEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn))
                .Average("averageLowCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T2))
                .Average("averageHighCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T1))
                .Average("averagePower", a => a
                  .Field(f => f.MeasurementData.powerIn))
                .Average("averagePowerL1", a => a
                  .Field(f => f.MeasurementData.powerInL1))
                .Average("averagePowerL2", a => a
                  .Field(f => f.MeasurementData.powerInL2))
                .Average("averagePowerL3", a => a
                  .Field(f => f.MeasurementData.powerInL3))
                .Average("averageCurrentL1", a => a
                  .Field(f => f.MeasurementData.currentL1))
                .Average("averageCurrentL2", a => a
                  .Field(f => f.MeasurementData.currentL2))
                .Average("averageCurrentL3", a => a
                  .Field(f => f.MeasurementData.currentL3))
                .Average("averageVoltageL1", a => a
                  .Field(f => f.MeasurementData.voltageL1))
                .Average("averageVoltageL2", a => a
                  .Field(f => f.MeasurementData.voltageL2))
                .Average("averageVoltageL3", a => a
                  .Field(f => f.MeasurementData.voltageL3))))))));

  private ISearchResponse<Measurement>
  SearchAverageDashboardMeasurementsByOwnerUserPerInterval(
      string ownerUserId,
      Period? period,
      TimeSpan interval) =>
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
        .Term(t => t.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Terms("measurementsByDeviceId", t => t
              .Field(f => f.DeviceData.DeviceId.Suffix("keyword"))
              .Aggregations(a => a
                .Average("averageEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn))
                .Average("averageLowCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T2))
                .Average("averageHighCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T1))
                .Average("averagePower", a => a
                  .Field(f => f.MeasurementData.powerIn))
                .Average("averagePowerL1", a => a
                  .Field(f => f.MeasurementData.powerInL1))
                .Average("averagePowerL2", a => a
                  .Field(f => f.MeasurementData.powerInL2))
                .Average("averagePowerL3", a => a
                  .Field(f => f.MeasurementData.powerInL3))
                .Average("averageCurrentL1", a => a
                  .Field(f => f.MeasurementData.currentL1))
                .Average("averageCurrentL2", a => a
                  .Field(f => f.MeasurementData.currentL2))
                .Average("averageCurrentL3", a => a
                  .Field(f => f.MeasurementData.currentL3))
                .Average("averageVoltageL1", a => a
                  .Field(f => f.MeasurementData.voltageL1))
                .Average("averageVoltageL2", a => a
                  .Field(f => f.MeasurementData.voltageL2))
                .Average("averageVoltageL3", a => a
                  .Field(f => f.MeasurementData.voltageL3))))))));

  private Task<ISearchResponse<Measurement>>
  SearchAverageDashboardMeasurementsByOwnerPerIntervalAsync(
      string ownerId,
      Period? period,
      TimeSpan interval) =>
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
        .Term(t => t.DeviceData.OwnerId.Suffix("keyword"), ownerId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Terms("measurementsByDeviceId", t => t
              .Field(f => f.DeviceData.DeviceId.Suffix("keyword"))
              .Aggregations(a => a
                .Average("averageEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn))
                .Average("averageLowCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T2))
                .Average("averageHighCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T1))
                .Average("averagePower", a => a
                  .Field(f => f.MeasurementData.powerIn))
                .Average("averagePowerL1", a => a
                  .Field(f => f.MeasurementData.powerInL1))
                .Average("averagePowerL2", a => a
                  .Field(f => f.MeasurementData.powerInL2))
                .Average("averagePowerL3", a => a
                  .Field(f => f.MeasurementData.powerInL3))
                .Average("averageCurrentL1", a => a
                  .Field(f => f.MeasurementData.currentL1))
                .Average("averageCurrentL2", a => a
                  .Field(f => f.MeasurementData.currentL2))
                .Average("averageCurrentL3", a => a
                  .Field(f => f.MeasurementData.currentL3))
                .Average("averageVoltageL1", a => a
                  .Field(f => f.MeasurementData.voltageL1))
                .Average("averageVoltageL2", a => a
                  .Field(f => f.MeasurementData.voltageL2))
                .Average("averageVoltageL3", a => a
                  .Field(f => f.MeasurementData.voltageL3))))))));

  private ISearchResponse<Measurement>
  SearchAverageDashboardMeasurementsByOwnerPerInterval(
      string ownerId,
      Period? period,
      TimeSpan interval) =>
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
        .Term(t => t.DeviceData.OwnerId.Suffix("keyword"), ownerId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Terms("measurementsByDeviceId", t => t
              .Field(f => f.DeviceData.DeviceId.Suffix("keyword"))
              .Aggregations(a => a
                .Average("averageEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn))
                .Average("averageLowCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T2))
                .Average("averageHighCostEnergy", a => a
                  .Field(f => f.MeasurementData.energyIn_T1))
                .Average("averagePower", a => a
                  .Field(f => f.MeasurementData.powerIn))
                .Average("averagePowerL1", a => a
                  .Field(f => f.MeasurementData.powerInL1))
                .Average("averagePowerL2", a => a
                  .Field(f => f.MeasurementData.powerInL2))
                .Average("averagePowerL3", a => a
                  .Field(f => f.MeasurementData.powerInL3))
                .Average("averageCurrentL1", a => a
                  .Field(f => f.MeasurementData.currentL1))
                .Average("averageCurrentL2", a => a
                  .Field(f => f.MeasurementData.currentL2))
                .Average("averageCurrentL3", a => a
                  .Field(f => f.MeasurementData.currentL3))
                .Average("averageVoltageL1", a => a
                  .Field(f => f.MeasurementData.voltageL1))
                .Average("averageVoltageL2", a => a
                  .Field(f => f.MeasurementData.voltageL2))
                .Average("averageVoltageL3", a => a
                  .Field(f => f.MeasurementData.voltageL3))))))));

  private Task<ISearchResponse<Measurement>>
  SearchAverageDashboardMeasurementsPerIntervalAsync(
      string deviceId,
      Period? period,
      TimeSpan interval) =>
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
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Average("averageEnergy", a => a
              .Field(f => f.MeasurementData.energyIn))
            .Average("averageLowCostEnergy", a => a
              .Field(f => f.MeasurementData.energyIn_T2))
            .Average("averageHighCostEnergy", a => a
              .Field(f => f.MeasurementData.energyIn_T1))
            .Average("averagePower", a => a
              .Field(f => f.MeasurementData.powerIn))
            .Average("averagePowerL1", a => a
              .Field(f => f.MeasurementData.powerInL1))
            .Average("averagePowerL2", a => a
              .Field(f => f.MeasurementData.powerInL2))
            .Average("averagePowerL3", a => a
              .Field(f => f.MeasurementData.powerInL3))
            .Average("averageCurrentL1", a => a
              .Field(f => f.MeasurementData.currentL1))
            .Average("averageCurrentL2", a => a
              .Field(f => f.MeasurementData.currentL2))
            .Average("averageCurrentL3", a => a
              .Field(f => f.MeasurementData.currentL3))
            .Average("averageVoltageL1", a => a
              .Field(f => f.MeasurementData.voltageL1))
            .Average("averageVoltageL2", a => a
              .Field(f => f.MeasurementData.voltageL2))
            .Average("averageVoltageL3", a => a
              .Field(f => f.MeasurementData.voltageL3))))));

  private ISearchResponse<Measurement>
  SearchAverageDashboardMeasurementsPerInterval(
      string deviceId,
      Period? period,
      TimeSpan interval) =>
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
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Average("averageEnergy", a => a
              .Field(f => f.MeasurementData.energyIn))
            .Average("averageLowCostEnergy", a => a
              .Field(f => f.MeasurementData.energyIn_T2))
            .Average("averageHighCostEnergy", a => a
              .Field(f => f.MeasurementData.energyIn_T1))
            .Average("averagePower", a => a
              .Field(f => f.MeasurementData.powerIn))
            .Average("averagePowerL1", a => a
              .Field(f => f.MeasurementData.powerInL1))
            .Average("averagePowerL2", a => a
              .Field(f => f.MeasurementData.powerInL2))
            .Average("averagePowerL3", a => a
              .Field(f => f.MeasurementData.powerInL3))
            .Average("averageCurrentL1", a => a
              .Field(f => f.MeasurementData.currentL1))
            .Average("averageCurrentL2", a => a
              .Field(f => f.MeasurementData.currentL2))
            .Average("averageCurrentL3", a => a
              .Field(f => f.MeasurementData.currentL3))
            .Average("averageVoltageL1", a => a
              .Field(f => f.MeasurementData.voltageL1))
            .Average("averageVoltageL2", a => a
              .Field(f => f.MeasurementData.voltageL2))
            .Average("averageVoltageL3", a => a
              .Field(f => f.MeasurementData.voltageL3))))));
}
