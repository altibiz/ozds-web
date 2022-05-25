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
            await IndexLogAsync(item.Next.Value
              .ToMissingDataLog(extraction.Device));
          }
          else if (item.Original.ShouldValidate)
          {
            await UpdateDeviceLastValidationAsync(
              extraction.Device.Id,
              item.Original.Due);
          }

          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run()
      .Then(() => IndexLogAsync(
        new(
          LogType.Load,
          extraction.Device.Id,
          new()
          {
            Period = extraction.Period
          })));

  public Task LoadMeasurementsAsync(
      EnrichedMeasurementExtractionAsync extraction) =>
    extraction.Items
      .ForEachValueTask(
        async item =>
        {
          if (item.Next.HasValue)
          {
            await IndexLogAsync(item.Next.Value
              .ToMissingDataLog(extraction.Device));
          }
          else if (item.Original.ShouldValidate)
          {
            await UpdateDeviceLastValidationAsync(
              extraction.Device.Id,
              item.Original.Due);
          }

          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run()
      .Then(() => IndexLogAsync(
        new(
          LogType.Load,
          extraction.Device.Id,
          new()
          {
            Period = extraction.Period
          })));

  public void LoadMeasurements(
      EnrichedMeasurementExtraction extraction) =>
    extraction.Items
      .ForEach(
        item =>
        {
          if (item.Next.HasValue)
          {
            IndexLog(item.Next.Value
              .ToMissingDataLog(extraction.Device));
          }
          else if (item.Original.ShouldValidate)
          {
            UpdateDeviceLastValidation(
              extraction.Device.Id,
              item.Original.Due);
          }

          IndexMeasurements(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run()
      .Return(() => IndexLog(
        new(
          LogType.Load,
          extraction.Device.Id,
          new()
          {
            Period = extraction.Period
          })));
}
