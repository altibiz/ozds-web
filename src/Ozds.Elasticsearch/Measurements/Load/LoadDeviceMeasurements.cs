using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public IEnumerable<Measurement> LoadDeviceMeasurements(
      Device device, Period? period = null);

  public Task<IEnumerable<Measurement>> LoadDeviceMeasurementsAsync(
      Device device, Period? period = null);
}

public partial class Client
{
  public IEnumerable<Measurement> LoadDeviceMeasurements(
      Device device, Period? period = null)
  {
    var task = LoadDeviceMeasurementsAsync(device, period);
    task.Wait();
    return task.Result;
  }

  public async Task<IEnumerable<Measurement>> LoadDeviceMeasurementsAsync(
      Device device, Period? period = null)
  {
    var provider = Providers.Find(p => p.Source == device.Source);
    if (provider is null) { return new List<Measurement> { }; }

    var measurements =
        (await provider.GetMeasurementsAsync(device, period)).ToList();

    Logger.LogDebug(
        $"Got {measurements.Count} measurements " + $"from {device.Id}");

    return measurements;
  }
}
