using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ozds.Elasticsearch
{
  public partial interface IClient : IBulkSortedMeasurementProvider { };

  public sealed partial class Client : IClient
  {
    public string Source { get => Client.s_source; }

    public IEnumerable<Measurement> GetMeasurements(Device device,
        Period? period = null) => SearchMeasurements(device, period).Sources();

    public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(
        Device device, Period? period = null) =>
        (await SearchMeasurementsAsync(device, period)).Sources();

    public IEnumerable<Measurement> GetMeasurementsSorted(
        Device device, Period? period = null) =>
        SearchMeasurementsSorted(device, period).Sources();

    public async Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
        Device device, Period? period = null) =>
        (await SearchMeasurementsSortedAsync(device, period)).Sources();

    public IEnumerable<Measurement> GetMeasurements(
        Period? period = null) => SearchMeasurements(period).Sources();

    public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(
        Period? period = null) =>
        (await SearchMeasurementsAsync(period)).Sources();

    public IEnumerable<Measurement> GetMeasurementsSorted(
        Period? period = null) => SearchMeasurementsSorted(period).Sources();

    public async Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
        Period? period = null) =>
        (await SearchMeasurementsSortedAsync(period)).Sources();

    private const string s_source = "Elasticsearch";
  }
}
