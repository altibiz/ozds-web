using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IBulkSortedMeasurementProvider : IBulkMeasurementProvider,
                                                  ISortedMeasurementProvider {
  public IEnumerable<Measurement> GetMeasurementsSorted(
      DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      DateTime? from = null, DateTime? to = null);
}
}
