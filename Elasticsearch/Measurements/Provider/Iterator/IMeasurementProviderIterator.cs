using System.Linq;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IMeasurementProviderIterator {
  public IEnumerable<IMeasurementProvider> Iterate();

  public IEnumerable<string> Sources {
    get => Iterate().Select(provider => provider.Source);
  }
}
}
