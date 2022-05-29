using Ozds.Extensions;

namespace Ozds.Elasticsearch;

// TODO: recovery when elasticsearch connection fails
public partial interface IClient : IMeasurementLoader { }

public partial class Client : IClient
{
  public Task LoadMeasurementsAwait(
      EnrichedMeasurementExtractionAsync extraction) =>
    extraction.Items
      .ForEachValueTask(
        async item =>
        {
          // NOTE: indexing first, so in case something happens right here, we
          // NOTE: don't have faulty logs
          // NOTE: duplication of measurements is not a problem ever and even
          // NOTE: if it was ids of measurements prevent that
          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));

          if (item.Next.HasValue)
          {
            await IndexMissingDataLogAsync(item.Next.Value
              .ToMissingDataLogFor(extraction.Device));

            var minimumMeasurements =
              extraction.Period.Span.TotalSeconds /
              extraction.Device.MeasurementInterval.TotalSeconds;
            Logger.LogDebug(
              $"Missing data for {extraction.Device.Id} " +
              $"at {extraction.Period} " +
              $"because {item.Next.Value.Error}");
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(item.Original.Error))
            {
              await DeleteMissingDataLogAsync(
                MissingDataLog.MakeId(
                  extraction.Device.Id,
                  item.Original.Period));

              Logger.LogDebug(
                $"Recovered missing data for {extraction.Device.Id} " +
                $"at {extraction.Period}");
            }

            if (item.Original.ShouldValidate)
            {
              await UpdateDeviceLastValidationAsync(
                extraction.Device.Id,
                item.Original.Due);

              Logger.LogDebug(
                $"Validated data for {extraction.Device.Id} " +
                $"at {extraction.Period}");
            }
          }
        })
      .Run()
      .ThenTask(() =>
        ExtendLoadLogPeriodAsync(
          LoadLog.MakeId(extraction.Device.Id),
          extraction.Period.To))
      .Then(() => Logger.LogDebug(
        $"Finished load for {extraction.Device.Id} " +
        $"at {extraction.Period}"));

  public Task LoadMeasurementsAsync(
      EnrichedMeasurementExtractionAsync extraction) =>
    extraction.Items
      .ForEachValueTask(
        async item =>
        {
          // NOTE: indexing first, so in case something happens right here, we
          // NOTE: don't have faulty logs
          // NOTE: duplication of measurements is not a problem ever and even
          // NOTE: if it was ids of measurements prevent that
          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));

          if (item.Next.HasValue)
          {
            await IndexMissingDataLogAsync(item.Next.Value
              .ToMissingDataLogFor(extraction.Device));

            Logger.LogDebug(
              $"Missing data for {extraction.Device.Id} " +
              $"at {extraction.Period} " +
              $"because {item.Next.Value.Error}");
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(item.Original.Error))
            {
              await DeleteMissingDataLogAsync(
                MissingDataLog.MakeId(
                  extraction.Device.Id,
                  item.Original.Period));

              Logger.LogDebug(
                $"Recovered missing data for {extraction.Device.Id} " +
                $"at {extraction.Period}");
            }

            if (item.Original.ShouldValidate)
            {
              await UpdateDeviceLastValidationAsync(
                extraction.Device.Id,
                item.Original.Due);

              Logger.LogDebug(
                $"Validated data for {extraction.Device.Id} " +
                $"at {extraction.Period}");
            }
          }
        })
      .Run()
      .ThenTask(() =>
        ExtendLoadLogPeriodAsync(
          LoadLog.MakeId(extraction.Device.Id),
          extraction.Period.To))
      .Then(() => Logger.LogDebug(
        $"Finished load for {extraction.Device.Id} at {extraction.Period}"));

  public void LoadMeasurements(
      EnrichedMeasurementExtraction extraction) =>
    extraction.Items
      .ForEach(
        item =>
        {
          // NOTE: indexing first, so in case something happens right here, we
          // NOTE: don't have faulty logs
          // NOTE: duplication of measurements is not a problem ever and even
          // NOTE: if it was ids of measurements prevent that
          IndexMeasurements(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));

          if (item.Next.HasValue)
          {
            IndexMissingDataLog(item.Next.Value
              .ToMissingDataLogFor(extraction.Device));

            Logger.LogDebug(
              $"Missing data for {extraction.Device.Id} " +
              $"at {extraction.Period} " +
              $"because {item.Next.Value.Error}");
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(item.Original.Error))
            {
              DeleteMissingDataLog(
                MissingDataLog.MakeId(
                  extraction.Device.Id,
                  item.Original.Period));

              Logger.LogDebug(
                $"Recovered missing data for {extraction.Device.Id} " +
                $"at {extraction.Period}");
            }

            if (item.Original.ShouldValidate)
            {
              UpdateDeviceLastValidation(
                extraction.Device.Id,
                item.Original.Due);

              Logger.LogDebug(
                $"Validated data for {extraction.Device.Id} " +
                $"at {extraction.Period}");
            }
          }
        })
      .Run()
      .Return(() =>
        ExtendLoadLogPeriod(
          LoadLog.MakeId(extraction.Device.Id),
          extraction.Period.To))
      .Return(() => Logger.LogDebug(
        $"Finished load for {extraction.Device.Id} at {extraction.Period}"));
}
