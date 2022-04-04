using Microsoft.Extensions.Logging;

namespace Elasticsearch.MeasurementFaker;

public sealed partial class Client : IClient
{
  public const string FakeSource = "fakeSource";
  public const string FakeDeviceId = "fakeDevice";

  public Client(ILogger<Client> logger) { Logger = logger; }

  private ILogger Logger { get; }
}
