using Ozds.Util;

namespace Ozds.Elasticsearch;

public partial interface IClient :
  IBulkSortedMeasurementProvider
{
}

public sealed partial class Client : IClient
{
  public string Source { get => Client.s_source; }

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      Device device,
      Period? period = null) =>
    SearchMeasurementsAsync(device.Id, period)
      .Then(measurements => measurements.Sources());

  public IEnumerable<Measurement> GetMeasurements(
      Device device,
      Period? period = null) =>
    SearchMeasurements(device.Id, period).Sources();

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      Device device,
      Period? period = null) =>
    SearchMeasurementsSortedAsync(device.Id, period)
      .Then(measurements => measurements.Sources());

  public IEnumerable<Measurement> GetMeasurementsSorted(
      Device device,
      Period? period = null) =>
    SearchMeasurementsSorted(device.Id, period).Sources();

  public Task<IEnumerable<Measurement>> GetMeasurementsAsync(
      Period? period = null) =>
    SearchMeasurementsAsync(period)
      .Then(measurements => measurements.Sources());

  public IEnumerable<Measurement> GetMeasurements(
      Period? period = null) =>
    SearchMeasurements(period).Sources();

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      Period? period = null) =>
    SearchMeasurementsSortedAsync(period)
      .Then(measurements => measurements.Sources());

  public IEnumerable<Measurement> GetMeasurementsSorted(
      Period? period = null) =>
    SearchMeasurementsSorted(period).Sources();

  private const string s_source = "Elasticsearch";
}
