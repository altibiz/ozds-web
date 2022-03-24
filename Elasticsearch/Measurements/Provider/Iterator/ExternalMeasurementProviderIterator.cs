using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch {
public class ExternalMeasurementProviderIterator
    : IMeasurementProviderIterator {
  public ExternalMeasurementProviderIterator() {
    _instances =
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

  public IEnumerable<IMeasurementProvider> Iterate() => _instances;

  private IEnumerable<IMeasurementProvider> _instances;
}
}
