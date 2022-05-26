using Xunit;
using Ozds.Util;

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

    // NOTE: ES needs some time to prepare for searching
    System.Threading.Thread.Sleep(1000);
    var searchResponse = Client.SearchDevicesBySource(
      Elasticsearch.MeasurementFaker.Client.FakeSource);
    Logger.LogDebug(searchResponse.DebugInformation);
    Assert.True(searchResponse.IsValid);

    var searchedDevices = searchResponse.Sources();
    Logger.LogDebug(searchedDevices.ToJson());
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

    // NOTE: ES needs some time to prepare for searching
    System.Threading.Thread.Sleep(1000);
    var searchResponse =
        await Client.SearchDevicesBySourceAsync(
            Elasticsearch.MeasurementFaker.Client.FakeSource);
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
