namespace Elasticsearch.Test {
  public static partial class Data {
    public static readonly Device MyEnergyCommunityDevice =
        new Device("MyEnergyCommunity", "M9EQCU59",
            new Device.KnownSourceDeviceData { ownerId = "test-owner" },
            DeviceState.Healthy);

    public static readonly Device FakeDevice =
        new Device(Elasticsearch.MeasurementFaker.Client.FakeSource,
            Elasticsearch.MeasurementFaker.Client.FakeDeviceId, null,
            DeviceState.Healthy);
  }
}
