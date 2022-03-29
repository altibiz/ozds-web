using System.Collections.Generic;

namespace Elasticsearch {
public class FakeMeasurementProviderIterator : IMeasurementProviderIterator {
  public IEnumerable<IMeasurementProvider> Iterate() => Instances;

  private IEnumerable<IMeasurementProvider> Instances { get; init; } =
    new List<IMeasurementProvider> { new MeasurementFaker.Client() };
}
}
