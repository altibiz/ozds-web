namespace Elasticsearch.MeasurementFaker {
  public sealed partial class Client : IClient {
    public static string FakeSource { get => "fakeSource"; }
    public static string FakeDeviceId { get => "fakeDevice"; }
  }
}
