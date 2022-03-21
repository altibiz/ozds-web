using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IMeasurementProvider {
  public IEnumerable<Measurement> getMeasurements(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> getMeasurementsAsync(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null);

  public IEnumerable<Measurement> getMeasurementsSorted(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> getMeasurementsSortedAsync(
      string ownerId, string deviceId, DateTime? from = null,
      DateTime? to = null);
}
}
