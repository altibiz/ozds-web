namespace Ozds.Elasticsearch.MeasurementFaker;

public sealed partial class Client : IClient
{
  public const string FakeSource = "FakeSource";
  public const string FakeDeviceId = "fakeDevice";
  public static readonly List<string> FakeDeviceIds =
    new List<string>
    {
      "fakeDevice1",
      "fakeDevice2",
      "fakeDevice3",
    };
  public const int MeasurementIntervalInSeconds = 15;

  public Client(ILogger<Client> logger) { Logger = logger; }

  private ILogger Logger { get; }
}
