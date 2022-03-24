namespace Elasticsearch.Test {
  public partial class Data {
    public static readonly Device TestDevice = new Device {
      DeviceId = "M9EQCU59",
      Source = "MyEnergyCommunity",
      SourceData = new Device.KnownSourceData { ownerId = "test-owner" },
      State = "healthy",
    };
  }
}
