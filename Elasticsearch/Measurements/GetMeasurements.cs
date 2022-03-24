using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch {
public partial interface IClient : IBulkSortedMeasurementProvider {};

public sealed partial class Client : IClient {
  public string Source { get => Client.s_source; }

  public IEnumerable<Measurement> GetMeasurements(Device device,
      DateTime? from = null,
      DateTime? to = null) => SearchMeasurements(device, from, to).Sources();

  public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      Device device, DateTime? from = null, DateTime? to = null) =>
      (await SearchMeasurementsAsync(device, from, to)).Sources();

  public IEnumerable<Measurement> GetMeasurementsSorted(
      Device device, DateTime? from = null, DateTime? to = null) =>
      SearchMeasurementsSorted(device, from, to).Sources();

  public async Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      Device device, DateTime? from = null, DateTime? to = null) =>
      (await SearchMeasurementsSortedAsync(device, from, to)).Sources();

  public IEnumerable<Measurement> GetMeasurements(DateTime? from = null,
      DateTime? to = null) => SearchMeasurements(from, to).Sources();

  public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      DateTime? from = null, DateTime? to = null) =>
      (await SearchMeasurementsAsync(from, to)).Sources();

  public IEnumerable<Measurement> GetMeasurementsSorted(DateTime? from = null,
      DateTime? to = null) => SearchMeasurementsSorted(from, to).Sources();

  public async Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      DateTime? from = null, DateTime? to = null) =>
      (await SearchMeasurementsSortedAsync(from, to)).Sources();

  private const string s_source = "Elasticsearch";
}
}
