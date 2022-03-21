using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

// TODO: optimize for dependency injection

namespace Elasticsearch {
public class MeasurementProviderIterator : IMeasurementProviderIterator {
  public IEnumerable<IMeasurementProvider> Iterate() =>
      Assembly.GetExecutingAssembly()
          .GetTypes()
          .Where(type => typeof(IMeasurementProvider).IsAssignableFrom(type) &&
                         !type.Equals(typeof(Elasticsearch.Client)))
          .Select(type => Activator.CreateInstance(type))
          .Select(instance => instance as IMeasurementProvider)
          .Aggregate(Enumerable.Empty<IMeasurementProvider>(),
              (accumulator, next) => next == null ? accumulator
                                                  : accumulator.Append(next));
}
}
