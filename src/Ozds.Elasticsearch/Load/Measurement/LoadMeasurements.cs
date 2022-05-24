using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient : IMeasurementLoader { }

public partial class Client : IClient
{
  public Task LoadMeasurementsAwait(
      IAsyncEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    measurements
      .ForEach(bucket => IndexMeasurementsAsync(
         bucket.Select(LoadMeasurementExtensions.ToMeasurement)))
      .Run();

  public Task LoadMeasurementsAsync(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    measurements
      .ForEach(bucket => IndexMeasurementsAsync(
         bucket.Select(LoadMeasurementExtensions.ToMeasurement)))
      .Run();

  public void LoadMeasurements(
      IEnumerable<IExtractionBucket<LoadMeasurement>> measurements) =>
    measurements
      .ForEach(bucket => IndexMeasurements(
         bucket.Select(LoadMeasurementExtensions.ToMeasurement)))
      .Run();
}
