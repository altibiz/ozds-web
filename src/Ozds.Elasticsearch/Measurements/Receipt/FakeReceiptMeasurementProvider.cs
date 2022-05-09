using Ozds.Util;

namespace Ozds.Elasticsearch;

public class FakeReceiptMeasurementProvider : IReceiptMeasurementProvider
{
  public Task<(EnergyMeasurement Begin, EnergyMeasurement End)>
  GetEnergyMeasurementsAsync(string deviceId, Period period) =>
    throw new NotImplementedException();

  public (EnergyMeasurement Begin, EnergyMeasurement End)
  GetEnergyMeasurements(string deviceId, Period period) =>
    GetEnergyMeasurementsAsync(deviceId, period).BlockTask();

  public Task<PowerMeasurement> GetPowerMeasurementAsync(
      string deviceId, Period period) =>
    throw new NotImplementedException();

  public PowerMeasurement GetPowerMeasurement(
      string deviceId, Period period) =>
    GetPowerMeasurementAsync(deviceId, period).BlockTask();
}
