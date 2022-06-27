namespace Ozds.Elasticsearch;

public class FakeDeviceLoader : IDeviceLoader
{
  public Task LoadDeviceAsync(LoadDevice device) => Task.CompletedTask;

  public void LoadDevice(LoadDevice device) { }
}
