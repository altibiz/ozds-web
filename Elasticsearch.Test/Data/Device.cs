namespace Elasticsearch.Test {
  public partial class Data {
    public static readonly Device TestDevice = new Device("MyEnergyCommunity",
        "M9EQCU59", new Device.KnownSourceDeviceData { ownerId = "test-owner" },
        "healthy");
  }
}
