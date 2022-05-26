using Xunit;

namespace Ozds.Elasticsearch.Test;

public static partial class Data
{
  public static readonly Measurement MyEnergyCommunityMeasurement =
    new Measurement(
      DateTime.UtcNow,
      new Measurement.DeviceDataType(
        "MyEnergyCommunity",
        "M9EQCU59",
        "HelbOzds",
        "TestCenterId",
        "TestCenterUserId",
        "TestOwnerId",
        "TestOwnerUserId"),
      new Measurement.MeasurementDataType
      {
        energyIn = 2803.013M,
        energyIn_T1 = 719.16M,
        energyIn_T2 = 2083.848M,
        powerIn = 0M,

        dongleId = "M9EQCU59",
        meterIdent = "ISK5/2M550T-2006",
        meterSerial = "83793906",
        localTime = "194938",
        localDate = "220322",
        energyOut = 0.785M,
        energyOut_T1 = 0.121M,
        energyOut_T2 = 0.664M,
        powerOut = 0M,
        powerInL1 = 0M,
        powerInL2 = 0M,
        powerInL3 = 0M,
        powerOutL1 = 0M,
        powerOutL2 = 0M,
        powerOutL3 = 0M,
        voltageL1 = 238.7M,
        voltageL2 = 240.2M,
        voltageL3 = 240.9M,
        currentL1 = 0M,
        currentL2 = 0M,
        currentL3 = 0M,
        numLongPwrFailures = 3,
        numVoltageSagsL1 = 0,
        numVoltageSagsL2 = 0,
        numVoltageSagsL3 = 0,
        numVoltageSwellsL1 = 0,
        numVoltageSwellsL2 = 0,
        numVoltageSwellsL3 = 0,
        limiter = -1,
        fuseSupervisionL1 = 0,
        disconnectControl = 1,
      });

  public static readonly Measurement FakeMeasurement =
    new Measurement(
      DateTime.UtcNow,
      new Measurement.DeviceDataType(
        "HelbOzds",
        "TestCenterId",
        "TestCenterUserId",
        "TestOwnerId",
        "TestOwnerUserId",
        Ozds.Elasticsearch.MeasurementFaker.Client.FakeSource,
        Ozds.Elasticsearch.MeasurementFaker.Client.FakeDeviceId),
      new Measurement.MeasurementDataType
      {
        energyIn = 2803.013M,
        energyIn_T1 = 719.16M,
        energyIn_T2 = 2083.848M,
        powerIn = 0M,
      });

  public static IEnumerable<object[]> GenerateMeasurements()
  {
    yield return
      new object[]
      {
        new Measurement[]
        {
          FakeMeasurement
        }
      };
  }

  public static IEnumerable<object[]> GenerateMeasurement()
  {
    yield return
      new object[]
      {
        FakeMeasurement
      };
  }
}

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public async Task SetupMeasurementsAsync(IEnumerable<Measurement> measurements)
  {
    foreach (var measurement in measurements)
    {
      await SetupMeasurementAsync(measurement);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public void SetupMeasurements(IEnumerable<Measurement> measurements)
  {
    foreach (var measurement in measurements)
    {
      SetupMeasurement(measurement);
    }
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurement), MemberType = typeof(Data))]
  public async Task SetupMeasurementAsync(Measurement measurement)
  {
    var measurementIndexResponse =
      await Client.IndexMeasurementAsync(measurement);
    Assert.True(measurementIndexResponse.IsValid);

    var indexedMeasurementId = measurementIndexResponse.Id;
    Assert.Equal(measurement.Id, indexedMeasurementId);

    var measurementGetResponse =
      await Client.GetMeasurementAsync(measurement.Id);
    Assert.True(measurementGetResponse.IsValid);

    var gotMeasurement = measurementGetResponse.Source;
    Assert.Equal(measurement, gotMeasurement);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurement), MemberType = typeof(Data))]
  public void SetupMeasurement(Measurement measurement)
  {
    var measurementIndexResponse = Client.IndexMeasurement(measurement);
    Assert.True(measurementIndexResponse.IsValid);

    var indexedMeasurementId = measurementIndexResponse.Id;
    Assert.Equal(measurement.Id, indexedMeasurementId);

    var measurementGetResponse = Client.GetMeasurement(measurement.Id);
    Assert.True(measurementGetResponse.IsValid);

    var gotMeasurement = measurementGetResponse.Source;
    Assert.Equal(measurement, gotMeasurement);
  }
}
