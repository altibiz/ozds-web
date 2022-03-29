using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch {
public class ExternalMeasurementProviderIterator
    : IMeasurementProviderIterator {
  public IEnumerable<IMeasurementProvider> Iterate() => Instances;

  private IEnumerable<IMeasurementProvider> Instances { get; init; } =
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(
                type => typeof(IMeasurementProvider).IsAssignableFrom(type) &&
                        !type.IsInterface && !type.Equals(typeof(Client)))
            .Select(type => Activator.CreateInstance(type))
            .Select(instance => instance as IMeasurementProvider)
            .Aggregate(Enumerable.Empty<IMeasurementProvider>(),
                (accumulator, next) =>
                    next == null ? accumulator : accumulator.Append(next));
}
}
