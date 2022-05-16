using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<IEnumerable<Measurement>> LoadSourceMeasurementsAsync(
      string source, Period? period = null);

  public IEnumerable<Measurement> LoadSourceMeasurements(
      string source, Period? period = null);
}

public partial class Client : IClient
{
  public Task<IEnumerable<Measurement>> LoadSourceMeasurementsAsync(
      string source, Period? period = null) =>
    period
      .WhenNonNullableFinally(
        Functions.Id,
        () => DetermineLoadPeriodAsync(source))
      .ThenTask(period =>
        Providers
          .Find(provider => provider.Source == source)
          .WhenNonNullableFinallyTask(provider =>
            provider
              .WithNullableTask(_ =>
                IndexLogAsync(
                  new Log(
                    LogType.LoadBegin,
                    provider.Source,
                    new Log.KnownData
                    {
                      Period = period
                    })))
              .ThenTask(provider => SearchDevicesAsync(provider.Source))
              .ThenTask(response =>
                response
                  .Sources()
                  .Select(device =>
                    LoadDeviceMeasurementsAsync(
                      device,
                      period))
                  .Await())
              .Then(Enumerables.Flatten)
              .Then(Enumerable.ToList)
              .ThenWith(measurements =>
                Logger.LogInformation(
                  $"Fetched {measurements.Count} measurements " +
                  $"from {provider.Source} for {period}"))
              .ThenWithTask(_ =>
                IndexLogAsync(
                  new Log(
                    LogType.LoadEnd,
                    provider.Source,
                    new Log.KnownData
                    {
                      Period = period
                    })))
              .Then(Enumerable.AsEnumerable),
            () => Enumerable.Empty<Measurement>().ToTask()));

  public IEnumerable<Measurement> LoadSourceMeasurements(
      string source, Period? period = null) =>
    period
      .WhenNonNullableFinally(
        Functions.Id,
        () => DetermineLoadPeriod(source))
      .WhenNullable(period =>
        Providers
          .Find(provider => provider.Source == source)
          .WhenNonNullableFinally(provider =>
            provider
              .WithNullable(_ =>
                IndexLog(
                  new Log(
                    LogType.LoadBegin,
                    provider.Source,
                    new Log.KnownData
                    {
                      Period = period
                    })))
              .WhenNullable(provider =>
                SearchDevices(provider.Source)
                  .Sources()
                  .SelectMany(device =>
                    LoadDeviceMeasurements(
                      device,
                      period))
                  .ToList())
              .WithNullable(measurements =>
                Logger.LogInformation(
                  $"Fetched {measurements.Count} measurements " +
                  $"from {provider.Source} for {period}"))
              .WithNullable(_ =>
                IndexLog(
                  new Log(
                    LogType.LoadEnd,
                    provider.Source,
                    new Log.KnownData
                    {
                      Period = period
                    })))
              .WhenNullable(Enumerable.AsEnumerable),
            () => Enumerable.Empty<Measurement>()));

  private Task<Period> DetermineLoadPeriodAsync(string source) =>
    SearchLoadLogsSortedByPeriodAsync(source, 1)
      .Then(logs =>
        (logs.Sources().FirstOrDefault()?.Data?.Period?.To,
         DateTime.UtcNow) switch
        {
          (DateTime from, DateTime to) =>
            new Period
            {
              From = from,
              To = to
            },
          (null, DateTime to) =>
            new Period
            {
              From = DateTime.MinValue.ToUniversalTime(),
              To = to
            }
            .WithNullable(_ =>
              {
                Logger.LogDebug($"Last load log not found for {source}");
                Logger.LogDebug(logs.DebugInformation);
              })
        });

  private Period DetermineLoadPeriod(string source) =>
    SearchLoadLogsSortedByPeriod(source, 1)
      .WhenNullable(logs =>
        (logs.Sources().FirstOrDefault()?.Data?.Period?.To,
         DateTime.UtcNow) switch
        {
          (DateTime from, DateTime to) =>
            new Period
            {
              From = from,
              To = to
            },
          (null, DateTime to) =>
            new Period
            {
              From = DateTime.MinValue.ToUniversalTime(),
              To = to
            }
            .WithNullable(_ =>
              {
                Logger.LogDebug($"Last load log not found for {source}");
                Logger.LogDebug(logs.DebugInformation);
              })
        });
}
