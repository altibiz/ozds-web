using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IMeasurementProvider {
  public IEnumerable<Measurement> GetMeasurements(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null);

  public IEnumerable<Measurement> GetMeasurementsSorted(string ownerId,
      string deviceId, DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      string ownerId, string deviceId, DateTime? from = null,
      DateTime? to = null);
}
}
