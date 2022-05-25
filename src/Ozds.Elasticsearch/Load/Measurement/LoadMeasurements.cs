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

          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run();

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

          await IndexMeasurementsAsync(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run();

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

          IndexMeasurements(item.Bucket
            .Select(LoadMeasurementExtensions.ToMeasurement));
        })
      .Run();
}
