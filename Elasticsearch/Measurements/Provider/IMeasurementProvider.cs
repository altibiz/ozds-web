using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IMeasurementProvider {
  public string Source { get; }

  public IEnumerable<Measurement> GetMeasurements(
      Device device, DateTime? from = null, DateTime? to = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      Device device, DateTime? from = null, DateTime? to = null);
}
}
