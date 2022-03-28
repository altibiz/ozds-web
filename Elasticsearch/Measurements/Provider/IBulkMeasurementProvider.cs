using System.Threading.Tasks;
using System.Collections.Generic;

namespace Elasticsearch {
public interface IBulkMeasurementProvider : IMeasurementProvider {
  public IEnumerable<Measurement> GetMeasurements(Period? period = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      Period? period = null);
}
}
