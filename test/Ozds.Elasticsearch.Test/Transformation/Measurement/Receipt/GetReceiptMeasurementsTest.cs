using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public async Task GetReceiptMeasurementsAsyncTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    await SetupMeasurementsAsync(device, measurements);

    var energyMeasurements = await Client
      .GetEnergyMeasurementsAsync(device.Id, period);
    Assert.Equal(
      measurements.First().Timestamp,
      energyMeasurements.Begin.Timestamp);
    Assert.Equal(
      measurements.Last().Timestamp,
      energyMeasurements.End.Timestamp);

    var powerMeasurement = await Client
      .GetPowerMeasurementAsync(device.Id, period);
    Assert.Equal(
      measurements
        .Select(measurement => measurement.MeasurementData.powerIn)
        .Max(),
      powerMeasurement.Power);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public void GetReceiptMeasurementsTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    SetupMeasurements(device, measurements);

    var energyMeasurements = Client
      .GetEnergyMeasurements(device.Id, period);
    Assert.Equal(
      measurements.First().Timestamp,
      energyMeasurements.Begin.Timestamp);
    Assert.Equal(
      measurements.Last().Timestamp,
      energyMeasurements.End.Timestamp);

    var powerMeasurement = Client
      .GetPowerMeasurement(device.Id, period);
    Assert.Equal(
      measurements
        .Select(measurement => measurement.MeasurementData.powerIn)
        .Max(),
      powerMeasurement.Power);
  }
}
