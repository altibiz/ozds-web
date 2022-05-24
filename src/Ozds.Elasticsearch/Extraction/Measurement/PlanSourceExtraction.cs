using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IMeasurementExtractor
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAwait(
      string source,
      Period? period = null);

  public Task<IEnumerable<ExtractionPlan>>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null);

  public IEnumerable<ExtractionPlan>
  PlanSourceExtraction(
      string source,
      Period? period = null);
}

public partial interface IClient : IMeasurementExtractor { }

public partial class Client : IClient
{
  public IAsyncEnumerable<ExtractionPlan>
  PlanSourceExtractionAwait(
      string source,
      Period? period = null) =>
    SearchDevicesBySourceAsync(source)
      .Then(devices => devices
        .Sources()
        .Select(device =>
          PlanDeviceExtractionAsync(
            device.ToExtractionDevice(),
            period))
        .ToAsync())
      .ToAsyncEnumerableNonNullable()
      .Flatten();

  public Task<IEnumerable<ExtractionPlan>>
  PlanSourceExtractionAsync(
      string source,
      Period? period = null) =>
    SearchDevicesBySourceAsync(source)
      .Then(devices => devices
        .Sources()
        .Select(device =>
          PlanDeviceExtractionAsync(
            device.ToExtractionDevice(),
            period))
        .Await())
      .FlattenTask();

  public IEnumerable<ExtractionPlan>
  PlanSourceExtraction(
      string source,
      Period? period = null) =>
    SearchDevicesBySource(source)
      .Sources()
      .Select(device =>
        PlanDeviceExtraction(
          device.ToExtractionDevice(),
          period));
}
