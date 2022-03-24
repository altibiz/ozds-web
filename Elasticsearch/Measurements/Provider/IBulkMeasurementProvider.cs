using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IBulkMeasurementProvider : IMeasurementProvider {
  public IEnumerable<Measurement> GetMeasurements(
      DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      DateTime? from = null, DateTime? to = null);
}
}
