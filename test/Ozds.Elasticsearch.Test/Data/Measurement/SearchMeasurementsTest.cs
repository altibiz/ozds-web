using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public void SearchMeasurementsTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    SetupMeasurements(device, measurements, period);

    var searchResponse = Client.SearchMeasurements(period);
    Assert.True(searchResponse.IsValid);

    var searchedMeasurements = searchResponse.Sources();
    AssertExtensions.ElementsEqual(measurements, searchedMeasurements);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public async Task SearchMeasurementsAsyncTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    await SetupMeasurementsAsync(device, measurements, period);

    var searchResponse =
        await Client.SearchMeasurementsAsync(period);
    Logger.LogDebug(searchResponse.DebugInformation);
    Assert.True(searchResponse.IsValid);

    var searchedMeasurements = searchResponse.Sources();
    Assert.Equal(measurements.Count(), searchedMeasurements.Count());
    AssertExtensions.ElementsEqual(measurements, searchedMeasurements);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public void SearchMeasurementsByDeviceTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    SetupMeasurements(device, measurements, period);

    var searchResponse = Client.SearchMeasurementsByDevice(device.Id);
    Assert.True(searchResponse.IsValid);

    var searchedMeasurements = searchResponse.Sources();
    AssertExtensions.ElementsEqual(measurements, searchedMeasurements);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateMeasurements), MemberType = typeof(Data))]
  public async Task SearchMeasurementsByDeviceAsyncTest(
      Device device,
      IEnumerable<Measurement> measurements,
      Period period)
  {
    await SetupMeasurementsAsync(device, measurements, period);

    var searchResponse = await Client
      .SearchMeasurementsByDeviceAsync(device.Id);
    Logger.LogDebug(searchResponse.DebugInformation);
    Assert.True(searchResponse.IsValid);

    var searchedMeasurements = searchResponse.Sources();
    AssertExtensions.ElementsEqual(measurements, searchedMeasurements);
  }
}
