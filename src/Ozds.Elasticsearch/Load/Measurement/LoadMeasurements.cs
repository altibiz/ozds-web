using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementLoader { }

public partial class Client : IClient
{
  public Task LoadMeasurementsAwait(
      EnrichedExtractionOutcomeAsync measurements) =>
    measurements.Items
      .ForEach(item => IndexMeasurementsAsync(
         item.Bucket.Select(LoadMeasurementExtensions.ToMeasurement)))
      .Run();

  public Task LoadMeasurementsAsync(
      EnrichedExtractionOutcomeAsync measurements) =>
    measurements.Items
      .ForEach(item => IndexMeasurementsAsync(
         item.Bucket.Select(LoadMeasurementExtensions.ToMeasurement)))
      .Run();

  public void LoadMeasurements(
      EnrichedExtractionOutcome measurements) =>
    measurements.Items
      .ForEach(item => IndexMeasurements(
         item.Bucket.Select(LoadMeasurementExtensions.ToMeasurement)))
      .Run();
}
