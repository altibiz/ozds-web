using System;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IMeasurementProviderIterator {
  public IEnumerable<IMeasurementProvider> Iterate();
}
}
