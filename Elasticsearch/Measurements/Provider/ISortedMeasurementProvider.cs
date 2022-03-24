using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface ISortedMeasurementProvider : IMeasurementProvider {
  public IEnumerable<Measurement> GetMeasurementsSorted(
      Device device, DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      Device device, DateTime? from = null, DateTime? to = null);
}
}
