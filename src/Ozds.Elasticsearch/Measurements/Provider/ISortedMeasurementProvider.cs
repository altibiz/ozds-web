using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ozds.Elasticsearch
{
  public interface ISortedMeasurementProvider : IMeasurementProvider
  {
    public IEnumerable<Measurement> GetMeasurementsSorted(
        Device device, Period? period = null);

    public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
        Device device, Period? period = null);
  }
}
