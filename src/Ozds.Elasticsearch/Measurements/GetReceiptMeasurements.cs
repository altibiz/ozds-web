namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<(Measurement, Measurement)?> GetMonthlyEnergyMeasurementsAsync(
      int year, int month);

  public Task<Measurement?> GetMonthlyPowerMeasurementAsync(
      int year, int month);

  public (Measurement, Measurement)? GetMonthlyEnergyMeasurements(
      int year, int month);

  public Measurement? GetMonthlyPowerMeasurement(
      int year, int month);
}

public partial class Client : IClient
{
  public Task<(Measurement, Measurement)?> GetMonthlyEnergyMeasurementsAsync(
      int year, int month) =>
    throw new NotImplementedException();

  public Task<Measurement?> GetMonthlyPowerMeasurementAsync(
      int year, int month) =>
    throw new NotImplementedException();

  public (Measurement, Measurement)? GetMonthlyEnergyMeasurements(
      int year, int month) =>
    throw new NotImplementedException();

  public Measurement? GetMonthlyPowerMeasurement(
      int year, int month) =>
    throw new NotImplementedException();
}
