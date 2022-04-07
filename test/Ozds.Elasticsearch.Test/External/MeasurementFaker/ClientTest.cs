using Microsoft.Extensions.Logging;

namespace Ozds.Elasticsearch.Test.MeasurementFaker;

using Ozds.Elasticsearch.MeasurementFaker;

public partial class ClientTest
{
  public ClientTest(IClient client, ILogger<ClientTest> logger)
  {
    Logger = logger;
    Client = client;
  }

  private IClient Client { get; }
  private ILogger Logger { get; }
}
