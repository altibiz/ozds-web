using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public void SearchDevicesTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id).ToIds();
    SetupDevices(devices);

    var indexResponse = Client.IndexDevices(devices);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(indexResponse.IsValid);

    var indexedMeasurementIds = indexResponse.Items.Ids();
    AssertExtensions.ElementsEqual(deviceIds, indexedMeasurementIds);

    var searchResponse = Client.SearchDevicesBySource(
      FakeMeasurementProvider.FakeSource);
    Assert.True(searchResponse.IsValid);

    var searchedDevices = searchResponse.Sources();
    AssertExtensions.Superset(devices, searchedDevices);

    var deleteResponse = Client.DeleteDevices(deviceIds);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(deleteResponse.IsValid);

    var deletedMeasurementIds = deleteResponse.Items.Ids();
    AssertExtensions.ElementsEqual(deviceIds, deletedMeasurementIds);
  }

  [Theory]
  [MemberData(nameof(Data.GenerateDevices), MemberType = typeof(Data))]
  public async Task SearchDevicesAsyncTest(IEnumerable<Device> devices)
  {
    var deviceIds = devices.Select(d => d.Id).ToIds();
    SetupDevices(devices);

    var indexResponse = await Client.IndexDevicesAsync(devices);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(indexResponse.IsValid);

    var indexedMeasurementIds = indexResponse.Items.Ids();
    AssertExtensions.ElementsEqual(deviceIds, indexedMeasurementIds);

    var searchResponse =
        await Client.SearchDevicesBySourceAsync(
            FakeMeasurementProvider.FakeSource);
    Assert.True(searchResponse.IsValid);

    var searchedDevices = searchResponse.Sources();
    AssertExtensions.Superset(devices, searchedDevices);

    var deleteResponse = await Client.DeleteDevicesAsync(deviceIds);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(deleteResponse.IsValid);

    var deletedMeasurementIds = deleteResponse.Items.Ids();
    AssertExtensions.ElementsEqual(deviceIds, deletedMeasurementIds);
  }
}
