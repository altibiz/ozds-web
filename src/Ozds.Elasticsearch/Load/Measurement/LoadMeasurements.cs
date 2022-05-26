using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementLoader { }

public partial class Client : IClient
{
  public Task LoadMeasurementsAwait(
      EnrichedMeasurementExtractionAsync extraction) =>
    extraction.Items
      .ForEachValueTask(
        async item =>
        {
          if (item.Next.HasValue)
          {
            await IndexMissingDataLogAsync(item.Next.Value
              .ToMissingDataLogFor(extraction.Device));
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(item.Original.Error))
            {
              await DeleteMissingDataLogAsync(
                MissingDataLog.MakeId(
                  extraction.Device.Id,
                  item.Original.Period));
            }

            if (item.Original.ShouldValidate)
            {
              await UpdateDeviceLastValidationAsync(
                extraction.Device.Id,
                item.Original.Due);
            }
          }

          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run()
      .ThenTask(() =>
        ExtendLoadLogPeriodAsync(
          LoadLog.MakeId(extraction.Device.Id),
          extraction.Period.To));

  public Task LoadMeasurementsAsync(
      EnrichedMeasurementExtractionAsync extraction) =>
    extraction.Items
      .ForEachValueTask(
        async item =>
        {
          if (item.Next.HasValue)
          {
            await IndexMissingDataLogAsync(item.Next.Value
              .ToMissingDataLogFor(extraction.Device));
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(item.Original.Error))
            {
              await DeleteMissingDataLogAsync(
                MissingDataLog.MakeId(
                  extraction.Device.Id,
                  item.Original.Period));
            }

            if (item.Original.ShouldValidate)
            {
              await UpdateDeviceLastValidationAsync(
                extraction.Device.Id,
                item.Original.Due);
            }
          }

          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run()
      .ThenTask(() =>
        ExtendLoadLogPeriodAsync(
          LoadLog.MakeId(extraction.Device.Id),
          extraction.Period.To));

  public void LoadMeasurements(
      EnrichedMeasurementExtraction extraction) =>
    extraction.Items
      .ForEach(
        item =>
        {
          if (item.Next.HasValue)
          {
            IndexMissingDataLog(item.Next.Value
              .ToMissingDataLogFor(extraction.Device));
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(item.Original.Error))
            {
              DeleteMissingDataLog(
                MissingDataLog.MakeId(
                  extraction.Device.Id,
                  item.Original.Period));
            }

            if (item.Original.ShouldValidate)
            {
              UpdateDeviceLastValidation(
                extraction.Device.Id,
                item.Original.Due);
            }
          }

          IndexMeasurements(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run()
      .Return(() =>
        ExtendLoadLogPeriod(
          LoadLog.MakeId(extraction.Device.Id),
          extraction.Period.To));
}
