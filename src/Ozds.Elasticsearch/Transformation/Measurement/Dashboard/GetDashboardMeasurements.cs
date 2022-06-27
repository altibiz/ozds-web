using Nest;

using Ozds.Extensions;

namespace Ozds.Elasticsearch;

// TODO: filter nulls

public partial interface IElasticsearchClient : IDashboardMeasurementProvider
{
}

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public const int MaxMeasurementsPerResponse = 400;
  public static readonly TimeSpan MaxShortPeriodSpan = TimeSpan.FromHours(1);
  public static readonly TimeSpan DefaultInterval = TimeSpan.FromDays(30);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByDeviceAsync(
      string deviceId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null ||
          longPeriod.Span > MaxShortPeriodSpan =>
        SearchAverageDashboardMeasurementsByDevicePerIntervalAsync(
            deviceId,
            longPeriod,
            longPeriod switch
            {
              Period exactPeriod =>
                exactPeriod.Span /
                MaxMeasurementsPerResponse,
              null => DefaultInterval
            })
          .Then(response => response
            .DashboardMeasurementsFromIntervalBuckets(deviceId)),
      (Period shortPeriod) =>
        SearchMeasurementsByDeviceAsync(deviceId, shortPeriod)
          .Then(response => response.Sources()
            .Select(measurement => measurement
              .ToDashboardMeasurement()))
          .ThenAwait(measurements =>
            GetCachedDeviceDashboardMeasurementsAsync(
                deviceId,
                CreateNearRealTimePeriod(
                  measurements,
                  shortPeriod))
              .ToValueTask()
              .Then(nearRealTimeMeasurements => measurements
                .Concat(nearRealTimeMeasurements))),
      // NOTE: it is covered but C# is complaining null is not covered
      _ => Enumerable.Empty<DashboardMeasurement>().ToTask(),
    };

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByDevice(
      string deviceId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null ||
          longPeriod.Span > MaxShortPeriodSpan =>
        SearchAverageDashboardMeasurementsByDevicePerInterval(
            deviceId,
            longPeriod,
            longPeriod switch
            {
              Period exactPeriod =>
                exactPeriod.Span /
                MaxMeasurementsPerResponse,
              null => DefaultInterval
            })
          .DashboardMeasurementsFromIntervalBuckets(deviceId),
      (Period shortPeriod) =>
        SearchMeasurementsByDevice(deviceId, shortPeriod)
          .Sources()
          .Select(measurement => measurement
            .ToDashboardMeasurement())
          .Var(measurements => measurements
          .Concat(GetCachedDeviceDashboardMeasurements(
            deviceId,
            CreateNearRealTimePeriod(
              measurements,
              shortPeriod)))),
      // NOTE: it is covered but C# is complaining null is not covered
      _ => Enumerable.Empty<DashboardMeasurement>(),
    };

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null ||
          longPeriod.Span > MaxShortPeriodSpan =>
        SearchAverageDashboardMeasurementsByOwnerPerIntervalAsync(
            ownerId,
            longPeriod,
            longPeriod switch
            {
              Period exactPeriod =>
                exactPeriod.Span /
                MaxMeasurementsPerResponse,
              null => DefaultInterval
            })
          .Then(response => response
            .DashboardMeasurementsFromIntervalDeviceBuckets()),
      (Period shortPeriod) =>
        SearchMeasurementsByOwnerAsync(ownerId, shortPeriod)
          .Then(response => response
            .Sources()
            .Select(measurement => measurement
              .ToDashboardMeasurement()))
          .ThenAwait(measurements =>
            GetCachedDeviceDashboardMeasurementsByOwnerAsync(
                ownerId,
                CreateNearRealTimePeriod(
                  measurements,
                  shortPeriod))
              .ToValueTask()
              .Then(nearRealTimeMeasurements => measurements
                .Concat(nearRealTimeMeasurements))),
      // NOTE: it is covered but C# is complaining null is not covered
      _ => Enumerable.Empty<DashboardMeasurement>().ToTask(),
    };

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByOwner(
      string ownerId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null ||
          longPeriod.Span > MaxShortPeriodSpan =>
        SearchAverageDashboardMeasurementsByOwnerPerInterval(
            ownerId,
            longPeriod,
            longPeriod switch
            {
              Period exactPeriod =>
                exactPeriod.Span /
                MaxMeasurementsPerResponse,
              null => DefaultInterval
            })
          .DashboardMeasurementsFromIntervalDeviceBuckets(),
      (Period shortPeriod) =>
        SearchMeasurementsByOwnerUser(ownerId, shortPeriod)
          .Sources()
          .Select(measurement => measurement
            .ToDashboardMeasurement())
          .Var(measurements => measurements
          .Concat(GetCachedDeviceDashboardMeasurementsByOwner(
            ownerId,
            CreateNearRealTimePeriod(
              measurements,
              shortPeriod)))),
      // NOTE: it is covered but C# is complaining null is not covered
      _ => Enumerable.Empty<DashboardMeasurement>(),
    };

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null ||
          longPeriod.Span > MaxShortPeriodSpan =>
        SearchAverageDashboardMeasurementsByOwnerUserPerIntervalAsync(
            ownerUserId,
            longPeriod,
            longPeriod switch
            {
              Period exactPeriod =>
                exactPeriod.Span /
                MaxMeasurementsPerResponse,
              null => DefaultInterval
            })
          .Then(response => response
            .DashboardMeasurementsFromIntervalDeviceBuckets()),
      (Period shortPeriod) =>
        SearchMeasurementsByOwnerUserAsync(ownerUserId, shortPeriod)
          .Then(response => response
            .Sources()
            .Select(measurement => measurement
              .ToDashboardMeasurement()))
          .ThenAwait(measurements =>
            GetCachedDeviceDashboardMeasurementsByOwnerUserAsync(
                ownerUserId,
                CreateNearRealTimePeriod(
                  measurements,
                  shortPeriod))
              .ToValueTask()
              .Then(nearRealTimeMeasurements => measurements
                .Concat(nearRealTimeMeasurements))),
      // NOTE: it is covered but C# is complaining null is not covered
      _ => Enumerable.Empty<DashboardMeasurement>().ToTask(),
    };

  public IEnumerable<DashboardMeasurement>
  GetDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period? period = null) =>
    period switch
    {
      (var longPeriod) when
          longPeriod is null ||
          longPeriod.Span > MaxShortPeriodSpan =>
        SearchAverageDashboardMeasurementsByOwnerUserPerInterval(
            ownerUserId,
            longPeriod,
            longPeriod switch
            {
              Period exactPeriod =>
                exactPeriod.Span /
                MaxMeasurementsPerResponse,
              null => DefaultInterval
            })
          .DashboardMeasurementsFromIntervalDeviceBuckets(),
      (Period shortPeriod) =>
        SearchMeasurementsByOwnerUser(ownerUserId, shortPeriod)
          .Sources()
          .Select(measurement => measurement
            .ToDashboardMeasurement())
          .Var(measurements => measurements
          .Concat(GetCachedDeviceDashboardMeasurementsByOwnerUser(
            ownerUserId,
            CreateNearRealTimePeriod(
              measurements,
              shortPeriod)))),
      // NOTE: it is covered but C# is complaining null is not covered
      _ => Enumerable.Empty<DashboardMeasurement>(),
    };
}

// TODO: decent sync versions
public partial class ElasticsearchClient
{
  private Period
  CreateNearRealTimePeriod(
      IEnumerable<DashboardMeasurement> storedMeasurements,
      Period requestedPeriod) =>
    storedMeasurements.EmptyEnumerable() ? requestedPeriod.ClonePeriod()
    : new Period
    {
      From = storedMeasurements
            .MaxBy(measurement => measurement.Timestamp).Timestamp
            .Var(timestamp =>
              timestamp == default ? requestedPeriod.From
              : timestamp),
      To = requestedPeriod.To,
    };

  private Task<IEnumerable<DashboardMeasurement>>
  GetCachedDeviceDashboardMeasurementsAsync(
      string deviceId,
      Period period) =>
    GetCachedDeviceAsync(deviceId)
      .ThenWhenNonNullAwait(device => Providers
        .Find(provider => provider.Source == device.Source)
        .WhenNonNull(provider => provider
          .GetMeasurementsAsync(
            device.ToProvisionDevice(),
            period)
          .SelectMany(bucket => bucket
            .Select(measurement => measurement
              .ToDashboardMeasurement())
            .ToAsyncEnumerable())
          .AwaitValue()
          .NullableTask()))
      .ThenWhenNull(Enumerable.Empty<DashboardMeasurement>());

  private IEnumerable<DashboardMeasurement>
  GetCachedDeviceDashboardMeasurements(
      string deviceId,
      Period period) =>
    GetCachedDeviceDashboardMeasurementsAsync(
        deviceId,
        period)
      .Block();

  private Task<IEnumerable<DashboardMeasurement>>
  GetCachedDeviceDashboardMeasurementsByOwnerAsync(
      string ownerId,
      Period period) =>
    GetCachedDevicesByOwnerAsync(ownerId)
      .ThenAwait(devices => devices
        .Select(device => Providers
          .Find(provider => provider.Source == device.Source)
          .WhenNonNull(provider => provider
            .GetMeasurementsAsync(
              device.ToProvisionDevice(),
              period)
            .SelectMany(bucket => bucket
              .Select(measurement => measurement
                .ToDashboardMeasurement())
              .ToAsyncEnumerable())
            .AwaitValue()))
        .AwaitValue()
        .Then(Enumerables.Flatten));

  private IEnumerable<DashboardMeasurement>
  GetCachedDeviceDashboardMeasurementsByOwner(
      string ownerId,
      Period period) =>
    GetCachedDeviceDashboardMeasurementsByOwnerAsync(
        ownerId,
        period)
      .Block();

  private Task<IEnumerable<DashboardMeasurement>>
  GetCachedDeviceDashboardMeasurementsByOwnerUserAsync(
      string ownerUserId,
      Period period) =>
    GetCachedDevicesByOwnerUserAsync(ownerUserId)
      .ThenAwait(devices => devices
        .Select(device => Providers
          .Find(provider => provider.Source == device.Source)
          .WhenNonNull(provider => provider
            .GetMeasurementsAsync(
              device.ToProvisionDevice(),
              period)
            .SelectMany(bucket => bucket
              .Select(measurement => measurement
                .ToDashboardMeasurement())
              .ToAsyncEnumerable())
            .AwaitValue()))
        .AwaitValue()
        .Then(Enumerables.Flatten));

  private IEnumerable<DashboardMeasurement>
  GetCachedDeviceDashboardMeasurementsByOwnerUser(
      string ownerUserId,
      Period period) =>
    GetCachedDeviceDashboardMeasurementsByOwnerUserAsync(
        ownerUserId,
        period)
      .Block();
}

public partial class ElasticsearchClient
{
  private Task<ISearchResponse<Measurement>>
  SearchAverageDashboardMeasurementsByDevicePerIntervalAsync(
      string deviceId,
      Period? period,
      TimeSpan interval) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .AverageDashboardMeasurementsByDevicePerInterval(
        deviceId,
        period,
        interval));

  private ISearchResponse<Measurement>
  SearchAverageDashboardMeasurementsByDevicePerInterval(
      string deviceId,
      Period? period,
      TimeSpan interval) =>
    Elastic.Search<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .AverageDashboardMeasurementsByDevicePerInterval(
        deviceId,
        period,
        interval));

  private Task<ISearchResponse<Measurement>>
  SearchAverageDashboardMeasurementsByOwnerPerIntervalAsync(
      string ownerId,
      Period? period,
      TimeSpan interval) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .AverageDashboardMeasurementsByOwnerPerInterval(
        ownerId,
        period,
        interval));

  private ISearchResponse<Measurement>
  SearchAverageDashboardMeasurementsByOwnerPerInterval(
      string ownerId,
      Period? period,
      TimeSpan interval) =>
    Elastic.Search<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .AverageDashboardMeasurementsByOwnerPerInterval(
        ownerId,
        period,
        interval));

  private Task<ISearchResponse<Measurement>>
  SearchAverageDashboardMeasurementsByOwnerUserPerIntervalAsync(
      string ownerUserId,
      Period? period,
      TimeSpan interval) =>
    Elastic.SearchAsync<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .AverageDashboardMeasurementsByOwnerPerInterval(
        ownerUserId,
        period,
        interval));

  private ISearchResponse<Measurement>
  SearchAverageDashboardMeasurementsByOwnerUserPerInterval(
      string ownerUserId,
      Period? period,
      TimeSpan interval) =>
    Elastic.Search<Measurement>(s => s
      .Index(MeasurementIndexName)
      .Size(0)
      .AverageDashboardMeasurementsByOwnerPerInterval(
        ownerUserId,
        period,
        interval));
}

internal static class DashboardMeasurementNestExtensions
{
  public static IEnumerable<DashboardMeasurement>
  DashboardMeasurementsFromIntervalBuckets(
      this ISearchResponse<Measurement> @this,
      string deviceId) =>
    @this.Aggregations
      .DateHistogram("measurementsPerInterval").Buckets
      .Select(bucket =>
        new DashboardMeasurement
        {
          Timestamp = bucket.Date,
          DeviceId = deviceId,
          Data = bucket
              .DashboardMeasurementDataFromAverageBuckets()
        });

  public static IEnumerable<DashboardMeasurement>
  DashboardMeasurementsFromIntervalDeviceBuckets(
      this ISearchResponse<Measurement> @this) =>
    @this.Aggregations
      .DateHistogram("measurementsPerInterval").Buckets
      .SelectMany(intervalBucket => intervalBucket
        .Terms("measurementsByDeviceId").Buckets
        .Select(deviceBucket =>
          new DashboardMeasurement
          {
            Timestamp = intervalBucket.Date,
            DeviceId = deviceBucket.Key,
            Data = deviceBucket
              .DashboardMeasurementDataFromAverageBuckets()
          }));

  public static SearchDescriptor<Measurement>
  AverageDashboardMeasurementsByDevicePerInterval(
      this SearchDescriptor<Measurement> @this,
      string deviceId,
      Period? period,
      TimeSpan interval) =>
    @this
      .Query(q => q
        .PeriodRange(period) && q
        .DeviceIdTerm(deviceId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .AverageDashboardMeasurement())));

  public static SearchDescriptor<Measurement>
  AverageDashboardMeasurementsByOwnerPerInterval(
      this SearchDescriptor<Measurement> @this,
      string ownerId,
      Period? period,
      TimeSpan interval) =>
    @this
      .Query(q => q
        .PeriodRange(period) && q
        .OwnerIdTerm(ownerId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Terms("measurementsByDeviceId", t => t
              .Field(f => f.DeviceData.DeviceId.Suffix("keyword"))
              .Aggregations(a => a.AverageDashboardMeasurement())))));

  public static SearchDescriptor<Measurement>
  AverageDashboardMeasurementsByOwnerUserPerInterval(
      this SearchDescriptor<Measurement> @this,
      string ownerUserId,
      Period? period,
      TimeSpan interval) =>
    @this
      .Query(q => q
        .PeriodRange(period) && q
        .OwnerUserIdTerm(ownerUserId))
      .Aggregations(a => a
        .DateHistogram("measurementsPerInterval", h => h
          .Field(f => f.Timestamp)
          .FixedInterval(interval)
          .Aggregations(a => a
            .Terms("measurementsByDeviceId", t => t
              .Field(f => f.DeviceData.DeviceId.Suffix("keyword"))
              .Aggregations(a => a.AverageDashboardMeasurement())))));

  public static AggregationContainerDescriptor<Measurement>
  AverageDashboardMeasurement(
      this AggregationContainerDescriptor<Measurement> @this) =>
    @this
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
        .Field(f => f.MeasurementData.voltageL3));

  public static DashboardMeasurementData
  DashboardMeasurementDataFromAverageBuckets(
      this BucketBase deviceBucket) =>
    new DashboardMeasurementData
    {
      Energy = (decimal?)deviceBucket
        .AverageBucket("averageEnergy")
        .Value ?? default,
      LowCostEnergy = (decimal?)deviceBucket
        .AverageBucket("averageLowCostEnergy")
        .Value ?? default,
      HighCostEnergy = (decimal?)deviceBucket
        .AverageBucket("averageHighCostEnergy")
        .Value ?? default,
      Power = (decimal?)deviceBucket
        .AverageBucket("averagePower")
        .Value ?? default,
      PowerL1 = (decimal?)deviceBucket
        .AverageBucket("averagePowerL1")
        .Value ?? default,
      PowerL2 = (decimal?)deviceBucket
        .AverageBucket("averagePowerL2")
        .Value ?? default,
      PowerL3 = (decimal?)deviceBucket
        .AverageBucket("averagePowerL3")
        .Value ?? default,
      CurrentL1 = (decimal?)deviceBucket
        .AverageBucket("averageCurrentL1")
        .Value ?? default,
      CurrentL2 = (decimal?)deviceBucket
        .AverageBucket("averageCurrentL2")
        .Value ?? default,
      CurrentL3 = (decimal?)deviceBucket
        .AverageBucket("averageCurrentL3")
        .Value ?? default,
      VoltageL1 = (decimal?)deviceBucket
        .AverageBucket("averageVoltageL1")
        .Value ?? default,
      VoltageL2 = (decimal?)deviceBucket
        .AverageBucket("averageVoltageL2")
        .Value ?? default,
      VoltageL3 = (decimal?)deviceBucket
        .AverageBucket("averageVoltageL3")
        .Value ?? default,
    };
}
